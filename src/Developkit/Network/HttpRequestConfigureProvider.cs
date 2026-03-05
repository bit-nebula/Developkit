using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Nebula.Developkit.Network;

public interface IHttpRequestConfigureProvider
{
    HttpRequestConfigure GetConfiguration(string name);
}

public class HttpRequestConfigureProvider : IHttpRequestConfigureProvider
{
    private readonly IServiceProvider serviceProvider;

    public HttpRequestConfigureProvider(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public HttpRequestConfigure GetConfiguration(string name)
    {
        var option = serviceProvider.GetRequiredService<IOptionsMonitor<HttpRequestConfigure>>();
        var config = option.Get(name);
        return config;
    }
}
