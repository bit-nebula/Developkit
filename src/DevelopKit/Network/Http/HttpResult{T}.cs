namespace BitNebula.DevelopKit.Network.Http;

/// <summary>
/// 统一 HTTP 响应体：Code + Message + Data。
/// </summary>
/// <typeparam name="T">Data 数据类型。</typeparam>
public class HttpResult<T> : HttpResult
{
    public HttpResult() { }

    public HttpResult(ResultCode code) : base(code) { }

    public HttpResult(ResultCode code, string message) : base(code, message) { }

    public HttpResult(ResultCode code, T data) : base(code)
    {
        Data = data;
    }

    public HttpResult(ResultCode code, string message, T data) : base(code, message)
    {
        Data = data;
    }

    public T? Data { get; set; }
}
