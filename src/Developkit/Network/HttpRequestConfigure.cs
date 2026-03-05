namespace Nebula.Developkit.Network;

public class HttpRequestConfigure
{
    public int RetryCount { get; set; } = 3;
    //public bool LogRequestHeader { get; set; } = false;
    public bool LogRequestBody { get; set; } = false;
    //public bool LogResponseHeader { get; set; } = false;
    public bool LogResponseBody { get; set; } = false;

    //public string AppId { get; set; } = string.Empty;
    //public string AppSecret { get; set; } = string.Empty;
    /// <summary>
    /// 域名
    /// </summary>
    public string Domain { get; set; } = null!;
    /// <summary>
    /// 超时时间
    /// </summary>
    /// <remarks>默认30秒</remarks>
    public int Timeout { get; set; } = 10;
    /// <summary>
    /// 接口列表
    /// </summary>
    public Dictionary<string, RequestEndpoint> Endpoints { get; set; } = [];
}

public class HttpRequestEntity
{
    public string AppId { get; set; } = string.Empty;
    public string AppSecret { get; set; } = string.Empty;
    /// <summary>
    /// 域名
    /// </summary>
    public string Domain { get; set; } = null!;
    /// <summary>
    /// 超时时间
    /// </summary>
    /// <remarks>默认30秒</remarks>
    public int Timeout { get; set; } = 10;
    /// <summary>
    /// 接口列表
    /// </summary>
    public Dictionary<string, RequestEndpoint> Endpoints { get; set; } = [];
}

public enum MethodType
{
    Get,
    Post,
}

public class RequestEndpoint
{
    public MethodType Method { get; set; }

    public string Url { get; set; } = null!;
}