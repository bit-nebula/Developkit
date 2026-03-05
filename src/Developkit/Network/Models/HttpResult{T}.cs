using BitNebula.Developkit.Network.Models;

namespace Nebula.Developkit.Network.Models;

public class HttpResult<T> : HttpResult
{
    public T? Data { get; set; }

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
