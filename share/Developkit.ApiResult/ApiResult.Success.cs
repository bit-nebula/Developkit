namespace BitNebula.Developkit.ApiResult;

public partial class ApiResult
{
    public static ApiResult Success()
    {
        return new ApiResult()
        {
            Code = ResultCode.Success,
            Status = true,
            Message = "Success",
        };
    }

    public static ApiResult Success(string message)
    {
        return new ApiResult()
        {
            Code = ResultCode.Success,
            Status = true,
            Message = message,
        };
    }

    public static ApiResult<T> Success<T>(T data)
    {
        return new ApiResult<T>()
        {
            Code = ResultCode.Success,
            Status = true,
            Message = "Success",
            Data = data,
        };
    }

    public static ApiResult<T> Success<T>(string message, T data)
    {
        return new ApiResult<T>()
        {
            Code = ResultCode.Success,
            Status = true,
            Message = message,
            Data = data,
        };
    }

}
