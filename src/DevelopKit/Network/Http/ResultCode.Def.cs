namespace BitNebula.DevelopKit.Network.Http;

public readonly partial struct ResultCode
{
    /// <summary>成功</summary>
    public static ResultCode Success { get; set; } = new(100, "Success", true);
    /// <summary>创建成功</summary>
    public static ResultCode Created { get; set; } = new(110, "Created", true);
    /// <summary>请求已接受（异步处理中）</summary>
    public static ResultCode Accepted { get; set; } = new(120, "Accepted", true);
    /// <summary>处理成功，无返回内容</summary>
    public static ResultCode NoContent { get; set; } = new(130, "NoContent", true);

    /// <summary>失败</summary>
    public static ResultCode Fail { get; set; } = new(200, "Fail", false);
    /// <summary>未知错误</summary>
    public static ResultCode UnknownError { get; set; } = new(201, "UnknownError", false);
    /// <summary>内部错误</summary>
    public static ResultCode InternalError { get; set; } = new(202, "InternalError", false);
    /// <summary>业务异常</summary>
    public static ResultCode BusinessError { get; set; } = new(203, "BusinessError", false);
    /// <summary>参数错误</summary>
    public static ResultCode InvalidParameter { get; set; } = new(204, "InvalidParameter", false);
    /// <summary>服务暂不可用</summary>
    public static ResultCode ServiceUnavailable { get; set; } = new(205, "ServiceUnavailable", false);

    /// <summary>未授权</summary>
    public static ResultCode Unauthorized { get; set; } = new(210, "Unauthorized", false);
    /// <summary>禁止访问</summary>
    public static ResultCode Forbidden { get; set; } = new(211, "Forbidden", false);

    /// <summary>资源不存在</summary>
    public static ResultCode NotFound { get; set; } = new(220, "NotFound", false);

    /// <summary>请求超时</summary>
    public static ResultCode Timeout { get; set; } = new(230, "Timeout", false);
    /// <summary>请求过于频繁</summary>
    public static ResultCode TooManyRequests { get; set; } = new(231, "TooManyRequests", false);
}
