using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BitNebula.Developkit.Network;

public interface IHttpRequest
{
    void SetName(string name);

    Task<TResponse> SendAsync<TRequest, TResponse>(string actionName, TRequest request, CancellationToken token = default)
        where TRequest : class
        where TResponse : class;
}

public partial class HttpRequest : IHttpRequest
{
    public string? Name { get; private set; }

    private readonly ILogger<HttpRequest> logger;
    private readonly IHttpRequestConfigureProvider configureProvider;
    private readonly IHttpClientFactory httpClientFactory;

    //public HttpRequest(string name, IServiceProvider serviceProvider)
    //{
    //    Name = name;
    //    logger = serviceProvider.GetRequiredService<ILogger<HttpRequest>>();
    //    configureProvider = serviceProvider.GetRequiredService<IHttpRequestConfigureProvider>();
    //    httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
    //}

    public HttpRequest(
        ILogger<HttpRequest> logger,
        IHttpRequestConfigureProvider configureProvider,
        IHttpClientFactory httpClientFactory
        )
    {
        this.logger = logger;
        this.configureProvider = configureProvider;
        this.httpClientFactory = httpClientFactory;
    }

    public void SetName(string name) => Name = name;

    public async Task<TResponse> SendAsync<TRequest, TResponse>(
        string actionName,
        TRequest request,
        CancellationToken token = default
        )
        where TRequest : class
        where TResponse : class
    {
        if (string.IsNullOrWhiteSpace(Name)) throw new InvalidOperationException("HttpRequest Name is not set. Please call SetName method to set it.");
        if (string.IsNullOrWhiteSpace(actionName)) throw new ArgumentNullException(nameof(actionName));

        var config = configureProvider.GetConfiguration(Name);

        if (!config.Endpoints.TryGetValue(actionName, out RequestEndpoint? endpoint) || string.IsNullOrEmpty(endpoint.Url))
        {
            throw new InvalidOperationException($"Endpoint for action '{actionName}' is not configured in HttpRequest '{Name}'.");
        }

        string targetUrl;
        if (endpoint.Method == MethodType.Get)
        {
            targetUrl = ToUrlParameter(endpoint.Url, request);
        }
        else
        {
            targetUrl = endpoint.Url;
        }
        //if (config.LogRequestHeader)
        {
            logger.LogInformation("Request url: {Method} -> {Url}", endpoint.Method, targetUrl);
        }

        HttpClient httpClient = httpClientFactory.CreateClient(Name);

        HttpRequestMessage requestMessage = new(GetMethod(endpoint.Method), targetUrl);
        if (endpoint.Method == MethodType.Post)
        {
            string requestJson = JsonConvert.SerializeObject(request);
            if (config.LogRequestBody) logger.LogInformation("Request content: {requestJson}", requestJson);
            requestMessage.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");
        }

        var responseMessage = await httpClient.SendAsync(requestMessage, token);
        responseMessage.EnsureSuccessStatusCode();

        var responseContent = await responseMessage.Content.ReadAsStringAsync(token);
        if (config.LogResponseBody) logger.LogInformation("Response content: {responseContent}", responseContent);

        return JsonConvert.DeserializeObject<TResponse>(responseContent) ?? throw new InvalidOperationException("Failed to deserialize response");
    }

    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static HttpMethod GetMethod(MethodType methodType)
    {
        return methodType switch
        {
            MethodType.Get => HttpMethod.Get,
            MethodType.Post => HttpMethod.Post,
            _ => throw new NotSupportedException($"Method type '{methodType}' is not supported."),
        };
    }

    private static string ToUrlParameter<Tparam>(string url, Tparam parame) where Tparam : class
    {
        if (parame is null) return url;

        Type type = parame.GetType();
        PropertyInfo[] properties = type.GetProperties();
        if (properties.Length == 0) return url;

        StringBuilder stringBuilder = new(url);
        stringBuilder.Append('?');
        foreach (PropertyInfo property in properties)
        {
            string name = property.Name;
            var attributes = property.GetCustomAttributes(typeof(JsonPropertyAttribute), false);
            foreach (var attribute in attributes)
            {
                string? tmpName = null;
                if (attribute is JsonPropertyAttribute newtonJsonProperty)
                {
                    tmpName = newtonJsonProperty.PropertyName ?? string.Empty;

                }
                else if (attribute is JsonPropertyNameAttribute systemJsonProperty)
                {
                    tmpName = systemJsonProperty.Name ?? string.Empty;
                }

                if (string.IsNullOrEmpty(tmpName))
                {
                    continue;
                }
                name = tmpName;
                break;
            }
            stringBuilder.Append($"{name}={property.GetValue(parame)}&");
        }
        stringBuilder.Remove(stringBuilder.Length - 1, 1);
        return stringBuilder.ToString();
    }
}
