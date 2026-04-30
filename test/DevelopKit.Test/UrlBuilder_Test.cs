using Newtonsoft.Json.Linq;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace BitNebula.DevelopKit.Test;

[TestClass]
public class UrlBuilder_Test
{
    [TestMethod]
    [DataRow("api", "v1", "user", "1", "api/v1/user/1")]
    [DataRow("api/", "/v1/", "/user/", "/1/", "api/v1/user/1")]
    [DataRow("/api/", "/v1/", "/user/", "/1/", "/api/v1/user/1")]
    [DataRow("/api", "/v1", "/user", "/1", "/api/v1/user/1")]
    [DataRow("/api", "v1", "user", "1", "/api/v1/user/1")]
    [DataRow("api/v1/user/1", "", "", "", "api/v1/user/1")]
    [DataRow("/api/v1/user/1", "", "", "", "/api/v1/user/1")]
    [DataRow("api", "v1/user", "", "1", "api/v1/user/1")]
    [DataRow("/api", "/v1/user", "", "/1", "/api/v1/user/1")]
    public void RelativeTest(string api, string version, string user, string value, string target)
    {
        var url = UrlBuilder.Create().Append(api).Append(version).Append(user).Append(value).Build();
        Assert.AreEqual(target, url);
    }

    [TestMethod]
    [DataRow(null, "example.com", null, "/api/v1/user/1", "http://example.com/api/v1/user/1")]
    [DataRow(null, "example.com", null, "api/v1/user/1", "http://example.com/api/v1/user/1")]
    [DataRow(null, "example.com", 9090, "api/v1/user/1", "http://example.com:9090/api/v1/user/1")]
    [DataRow(true, "example.com", 9090, "api/v1/user/1", "https://example.com:9090/api/v1/user/1")]
    [DataRow(true, "example.com", 9090, "/////api/v1/user/1", "https://example.com:9090/api/v1/user/1")]
    [DataRow(null, null, 9090, "/////api/v1/user/1", "/api/v1/user/1")]
    [DataRow(true, null, 9090, "/////api/v1/user/1", "/api/v1/user/1")]
    [DataRow(false, null, 9090, "/////api/v1/user/1", "/api/v1/user/1")]
    [DataRow(false, "example.com", 9090, "/////api/v1/user/1", "http://example.com:9090/api/v1/user/1")]
    [DataRow(null, "example.com", 9090, "/////api/v1/user/1", "http://example.com:9090/api/v1/user/1")]
    [DataRow(true, "example.com", 9090, "/////api/v1/user/1", "https://example.com:9090/api/v1/user/1")]
    public void AbsoluteTest(bool? useSSL, string? host, int? port, string? path, string? expected)
    {
        var builder = UrlBuilder.Create();
        if (useSSL.HasValue)
        {
            builder = builder.WithSSL(useSSL.Value);
        }
        if (host is not null)
        {
            builder = builder.WithHost(host);
        }
        if (port.HasValue)
        {
            builder = builder.WithPort(port.Value);
        }
        if (path is not null)
        {
            builder = builder.Append(path);
        }

        var url = builder.Build();
        Assert.AreEqual(expected, url);
    }
}
