namespace BitNebula.Developkit.Network.Models;

public partial class HttpResult
{
    public static HttpResult Fail()
    {
        return new HttpResult()
        {
            Code = ResultCode.Fail,
            Status = false,
            Message = nameof(ResultCode.Fail),
        };
    }

    public static HttpResult Fail(string message)
    {
        return new HttpResult()
        {
            Code = ResultCode.Fail,
            Status = false,
            Message = message,
        };
    }
}
