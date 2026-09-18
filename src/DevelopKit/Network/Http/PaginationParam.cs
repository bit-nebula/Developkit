namespace BitNebula.DevelopKit.Network.Http;

/// <summary>
/// 分页请求参数（入站 DTO）。
/// PageIndex 默认 1（1 基，未设置或 ≤ 0 时按 1 处理）；
/// PageSize 未设置或 ≤ 0 时回退到 <see cref="PaginationOptions.DefaultPageSize"/>（默认 10，可全局设置一次）。
/// </summary>
public class PaginationParam
{
    private int _pageIndex;
    private int _pageSize;

    /// <summary>当前页码（1 基，从 1 开始）。未设置或 ≤ 0 时按 1 处理。</summary>
    public int PageIndex
    {
        get => _pageIndex <= 0 ? 1 : _pageIndex;
        set => _pageIndex = value;
    }

    /// <summary>每页条数。未设置或 ≤ 0 时回退到 PaginationOptions.DefaultPageSize。</summary>
    public int PageSize
    {
        get => _pageSize <= 0 ? PaginationOptions.DefaultPageSize : _pageSize;
        set => _pageSize = value;
    }
}
