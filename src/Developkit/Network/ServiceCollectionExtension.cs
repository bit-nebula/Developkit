using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BitNebula.Developkit.Network;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddHttpRequest(this IServiceCollection services, Action<HttpRequestConfigure> configureServiceOptions)
    {
        return services.AddHttpRequest(HttpRequestConstant.DefaultHttpClientName, configureServiceOptions);
    }

    public static IServiceCollection AddHttpRequest(this IServiceCollection services, string name, Action<HttpRequestConfigure> configureServiceOptions)
    {
        services.Configure(name, configureServiceOptions);
        services.AddHttpClient(name, (serviceProvide, httpClient) =>
        {
            var configureProvider = serviceProvide.GetRequiredService<IHttpRequestConfigureProvider>();
            var config = configureProvider.GetConfiguration(name);
            httpClient.BaseAddress = new Uri(config.Domain);
            httpClient.Timeout = TimeSpan.FromSeconds(config.Timeout);
        });
        services.TryAddSingleton<IHttpRequestFactory, HttpRequestFactory>();
        services.TryAddSingleton<IHttpRequestConfigureProvider, HttpRequestConfigureProvider>();
        services.TryAddKeyedSingleton<IHttpRequest, HttpRequest>(name);
        return services;
    }

    public static IServiceCollection AddHttpRequest(this IServiceCollection services, string name, IConfiguration configuration)
    {
        var configurationSection = configuration.GetSection(name);
        return services.AddHttpRequest(name, configurationSection);
    }

    public static IServiceCollection AddHttpRequest(this IServiceCollection services, string name, IConfigurationSection configurationSection)
    {
        services.AddOptions<HttpRequestConfigure>(name).Bind(configurationSection);
        services.AddHttpClient(name, (serviceProvide, httpClient) =>
        {
            var configureProvider = serviceProvide.GetRequiredService<IHttpRequestConfigureProvider>();
            var config = configureProvider.GetConfiguration(name);
            httpClient.BaseAddress = new Uri(config.Domain);
            httpClient.Timeout = TimeSpan.FromSeconds(config.Timeout);
        });
        services.TryAddSingleton<IHttpRequestFactory, HttpRequestFactory>();
        services.TryAddSingleton<IHttpRequestConfigureProvider, HttpRequestConfigureProvider>();
        services.TryAddKeyedSingleton<IHttpRequest, HttpRequest>(name);
        return services;
    }

    //public static IServiceCollection AddMulitpleHttpRequest(this IServiceCollection services, Action<Dictionary<string, HttpRequestConfigure>> configureServiceOptions)
    //{
    //    var requestOptions = new Dictionary<string, HttpRequestConfigure>();
    //    configureServiceOptions(requestOptions);
    //    return services.AddMulitpleHttpRequest(requestOptions);
    //}

    //public static IServiceCollection AddMulitpleHttpRequest(this IServiceCollection services, Dictionary<string, HttpRequestConfigure> requestOptions)
    //{
    //    foreach (var item in requestOptions)
    //    {
    //        services.AddHttpClient(item.Key, httpClient =>
    //        {
    //            httpClient.BaseAddress = new Uri(item.Value.Domain);
    //            httpClient.Timeout = TimeSpan.FromSeconds(item.Value.Timeout);
    //        });
    //        services.AddKeyedSingleton<IHttpRequest, HttpRequest>(item.Key);
    //    }
    //    return services;
    //}
}
