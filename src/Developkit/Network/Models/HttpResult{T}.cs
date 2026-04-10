namespace BitNebula.DevelopKit.Network.Models;

public class HttpResult<T> : HttpResult
{
    public new T? Data { get; set; }

    public HttpResult() { }

    public HttpResult(T data)
    {
        Data = data;
    }

    public HttpResult(ResultCode resultCode, string message, T data)
    {
        Code = resultCode;
        Message = message;
        Data = data;
    }
}
