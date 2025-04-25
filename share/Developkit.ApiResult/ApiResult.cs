namespace BitNebula.Developkit.ApiResult;

public partial class ApiResult
{
    private bool? _status = null;
    public bool Status
    {
        get => _status ?? Code == ResultCode.Success;
        set => _status = value;
    }

    public ResultCode Code { get; set; }
    public string Message { get; set; } = null!;
    public ApiResult() { }

    public ApiResult(ResultCode resultCode, string message)
    {
        Code = resultCode;
        Message = message;
    }
}
