namespace BitNebula.DevelopKit.Network.Models;

public enum ResultCode
{
    Error = -1,
    Success = 0,
    Fail = 1,
    Unauthorized = 2,
}

public class HttpResultCode
{
    public int Code { get; set; }

    public string Message { get; set; } = string.Empty;

    public HttpResultCode() { }

    public HttpResultCode(int code, string message)
    {
        Code = code;
        Message = message ?? throw new ArgumentNullException(nameof(message));
    }

    public static HttpResultCode Error { get; } = new(-1, "Error");
}
