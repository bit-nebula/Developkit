using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Nebula.DevelopKit.Web;

public static class CrossPlatformHostingExtensions
{
    public static IServiceCollection AddCrossPlatformHostingService(this IServiceCollection services)
    {
        return AddCrossPlatformHostingService(services, string.Empty);
    }

    public static IServiceCollection AddCrossPlatformHostingService(this IServiceCollection services, string serviceName)
    {
        if (OperatingSystem.IsWindows())
        {
            services.AddWindowsService(options =>
            {
                if (!string.IsNullOrEmpty(serviceName))
                {
                    options.ServiceName = serviceName;
                }
            });
        }
        else if (OperatingSystem.IsLinux())
        {
            services.AddSystemd();
        }
        return services;
    }
}
