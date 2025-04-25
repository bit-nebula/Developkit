namespace BitNebula.Developkit.ApiResult;

public class ApiResult<T> : ApiResult
{
    public T? Data { get; set; }

    public ApiResult() { }

    public ApiResult(T data)
    {
        Data = data;
    }

    public ApiResult(ResultCode resultCode, string message, T data)
    {
        Code = resultCode;
        Message = message;
        Data = data;
    }
}
