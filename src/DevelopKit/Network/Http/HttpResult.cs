namespace BitNebula.DevelopKit.Network.Http;

public abstract class HttpResult
{
    public HttpResult() { }

    public HttpResult(ResultCode code)
    {
        Code = code.Value;
        Message = code.Name;
    }

    public HttpResult(ResultCode code, string message)
    {
        Code = code.Value;
        Message = message;
    }

    public int Code { get; set; }

    public string Message { get; set; } = null!;

    public bool Status { get => Code >= 100 && Code < 200; }

    public static HttpResult<T> Create<T>(ResultCode resultCode) => new(resultCode);
    public static HttpResult<T> Create<T>(ResultCode resultCode, string message) => new(resultCode, message);
    public static HttpResult<T> Create<T>(ResultCode resultCode, T data) => new(resultCode, data: data);
    public static HttpResult<T> Create<T>(ResultCode resultCode, string message, T data) => new(resultCode, message, data);

    public static HttpResult<T> Success<T>() => new(ResultCode.Success);
    public static HttpResult<T> Success<T>(string message) => new(ResultCode.Success, message);
    public static HttpResult<T> Success<T>(T data) => new(ResultCode.Success, data);
    public static HttpResult<T> Success<T>(string message, T data) => new(ResultCode.Success, message, data);

    public static HttpResult<T> Fail<T>() => new(ResultCode.Fail);
    public static HttpResult<T> Fail<T>(string message) => new(ResultCode.Fail, message);
    public static HttpResult<T> Fail<T>(T data) => new(ResultCode.Fail, data);
    public static HttpResult<T> Fail<T>(string message, T data) => new(ResultCode.Fail, message, data);

    #region PagedResult Success

    public static HttpResult<Pagination<T>> PagedResult<T>(IEnumerable<T> datas, int totalCount)
    {
        var page = Pagination.Create(datas, totalCount);
        var result = Success(page);
        return result;
    }

    public static HttpResult<Pagination<T>> PagedResult<T>(IEnumerable<T> datas, int totalCount, string message)
    {
        var page = Pagination.Create(datas, totalCount);
        var result = Success(message, page);
        return result;
    }

    public static HttpResult<Pagination<T>> PagedResult<T>(IEnumerable<T> datas, int pageIndex, int totalCount)
    {
        var page = Pagination.Create(datas, pageIndex, totalCount);
        var result = Success(page);
        return result;
    }

    public static HttpResult<Pagination<T>> PagedResult<T>(IEnumerable<T> datas, int pageIndex, int totalCount, string message)
    {
        var page = Pagination.Create(datas, pageIndex, totalCount);
        var result = Success(message, page);
        return result;
    }

    public static HttpResult<Pagination<T>> PagedResult<T>(IEnumerable<T> datas, int pageIndex, int pageSize, int totalCount)
    {
        var page = Pagination.Create(datas, pageIndex, pageSize, totalCount);
        var result = Success(page);
        return result;
    }

    public static HttpResult<Pagination<T>> PagedResult<T>(IEnumerable<T> datas, int pageIndex, int pageSize, int totalCount, string message)
    {
        var page = Pagination.Create(datas, pageIndex, pageSize, totalCount);
        var result = Success(message, page);
        return result;
    }

    #endregion

    public static HttpResult<Pagination<T>> PagedResult<T>(ResultCode resultCode, IEnumerable<T> datas, int totalCount)
    {
        var page = Pagination.Create(datas, totalCount);
        var result = Create(resultCode, page);
        return result;
    }

    public static HttpResult<Pagination<T>> PagedResult<T>(ResultCode resultCode, IEnumerable<T> datas, int totalCount, string message)
    {
        var page = Pagination.Create(datas, totalCount);
        var result = Create(resultCode, message, page);
        return result;
    }

    public static HttpResult<Pagination<T>> PagedResult<T>(ResultCode resultCode, IEnumerable<T> datas, int pageIndex, int totalCount)
    {
        var page = Pagination.Create(datas, pageIndex, totalCount);
        var result = Create(resultCode, page);
        return result;
    }

    public static HttpResult<Pagination<T>> PagedResult<T>(ResultCode resultCode, IEnumerable<T> datas, int pageIndex, int totalCount, string message)
    {
        var page = Pagination.Create(datas, pageIndex, totalCount);
        var result = Create(resultCode, message, page);
        return result;
    }

    public static HttpResult<Pagination<T>> PagedResult<T>(ResultCode resultCode, IEnumerable<T> datas, int pageIndex, int pageSize, int totalCount)
    {
        var page = Pagination.Create(datas, pageIndex, pageSize, totalCount);
        var result = Create(resultCode, page);
        return result;
    }

    public static HttpResult<Pagination<T>> PagedResult<T>(ResultCode resultCode, IEnumerable<T> datas, int pageIndex, int pageSize, int totalCount, string message)
    {
        var page = Pagination.Create(datas, pageIndex, pageSize, totalCount);
        var result = Create(resultCode, message, page);
        return result;
    }
}
