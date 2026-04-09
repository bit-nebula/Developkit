namespace BitNebula.DevelopKit.Network.Models;

public partial class HttpResult
{
    private bool? _status = null;
    public bool Status
    {
        get => _status ?? Code == ResultCode.Success;
        set => _status = value;
    }

    public ResultCode Code { get; set; }
    public string Message { get; set; } = null!;
    public HttpResult() { }

    public HttpResult(ResultCode resultCode, string message)
    {
        Code = resultCode;
        Message = message;
    }
}
