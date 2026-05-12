namespace BitNebula.DevelopKit.Test;

[TestClass]
public class UrlBuilder_Test
{
    [TestMethod]
    [DataRow("http://", "", "", "")]
    [DataRow("http://example.com", "", "", "http://example.com")]
    [DataRow("http://example.com", "api", "Control", "http://example.com/api/Control")]
    [DataRow("http://example.com", "/api", "/Control", "http://example.com/api/Control")]
    [DataRow("http://example.com", "/api/", "/Control/", "http://example.com/api/Control")]
    [DataRow("http://example.com", "//api/", "/Control/", "http://example.com/api/Control")]
    [DataRow("/prefix", "//api/", "/Control/", "/prefix/api/Control")]
    [DataRow("https://example.com", "", "", "https://example.com")]
    [DataRow("http://example.com/", "", "", "http://example.com")]
    [DataRow("https://example.com/", "", "", "https://example.com")]
    [DataRow("http://example.com:9090/", "", "", "http://example.com:9090")]
    [DataRow("https://example.com:9090/", "", "", "https://example.com:9090")]
    [DataRow("http://example.com/api", "", "", "http://example.com/api")]
    [DataRow("http://example.com/api/", "", "", "http://example.com/api")]
    [DataRow("http://example.com/api/Control/Action?", "", "", "http://example.com/api/Control/Action")]
    [DataRow("http://example.com/api/Control/Action?a=", "", "", "http://example.com/api/Control/Action")]
    [DataRow("https://example.com/api/Control/Action?a=", "", "", "https://example.com/api/Control/Action")]
    [DataRow("https://example.com/api/Control/Action?a=", "/Subs", "/SubAction", "https://example.com/api/Control/Action/Subs/SubAction")]
    public void CreateTest(string baseUrl, string p1, string p2, string expected)
    {
        var url = UrlBuilder.Create(baseUrl).Append(p1).Append(p2).Build();
        Assert.AreEqual(expected, url);
    }

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
    [DataRow("/api", "/v1/u ser", "", "/1", "/api/v1/user/1")]
    public void RelativeTest(string api, string version, string user, string value, string expected)
    {
        var url = UrlBuilder.Create().Append(api).Append(version).Append(user).Append(value).Build();
        Assert.AreEqual(expected, url);
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
