using Microsoft.Extensions.DependencyInjection;

namespace Nebula.Developkit.Network;

public interface IHttpRequestFactory
{
    IHttpRequest Create();

    IHttpRequest Create(string name);
}

public class HttpRequestFactory : IHttpRequestFactory
{
    private readonly IServiceProvider serviceProvider;

    public HttpRequestFactory(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public IHttpRequest Create()
    {
        return Create(HttpRequestConstant.DefaultHttpClientName);
    }

    public IHttpRequest Create(string name)
    {
        IHttpRequest httpRequest = serviceProvider.GetRequiredKeyedService<IHttpRequest>(name);
        httpRequest.SetName(name);

        return httpRequest;
    }
}
