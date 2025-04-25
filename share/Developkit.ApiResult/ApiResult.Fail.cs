namespace BitNebula.Developkit.ApiResult;

public partial class ApiResult
{
    public static ApiResult Fail()
    {
        return new ApiResult()
        {
            Code = ResultCode.Fail,
            Status = false,
            Message = nameof(ResultCode.Fail),
        };
    }

    public static ApiResult Fail(string message)
    {
        return new ApiResult()
        {
            Code = ResultCode.Fail,
            Status = false,
            Message = message,
        };
    }
}
