using Nebula.DevelopKit.Network.Models;

namespace BitNebula.DevelopKit.Network.Models;

public partial class HttpResult
{
    public static HttpResult Success()
    {
        return new HttpResult()
        {
            Code = ResultCode.Success,
            Status = true,
            Message = nameof(ResultCode.Success),
        };
    }

    public static HttpResult Success(string message)
    {
        return new HttpResult()
        {
            Code = ResultCode.Success,
            Status = true,
            Message = message,
        };
    }

    public static HttpResult<T> Success<T>(T data)
    {
        return new HttpResult<T>()
        {
            Code = ResultCode.Success,
            Status = true,
            Message = nameof(ResultCode.Success),
            Data = data,
        };
    }

    public static HttpResult<T> Success<T>(string message, T data)
    {
        return new HttpResult<T>()
        {
            Code = ResultCode.Success,
            Status = true,
            Message = message,
            Data = data,
        };
    }

}
