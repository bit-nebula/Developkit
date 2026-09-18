namespace BitNebula.DevelopKit.Network.Http;

/// <summary>
/// 分页元数据模型：描述当前页、每页大小与总量等信息。
/// 所有派生字段（总页数 / 是否有上、下一页）均由 <see cref="PageIndex"/>、<see cref="PageSize"/>、<see cref="TotalCount"/> 推导，
/// 反序列化后会随这三个基础字段自动重算，无需持久化派生值。
/// </summary>
public abstract class Pagination
{
    /// <summary>当前页码（1 基，从 1 开始）。</summary>
    public int PageIndex { get; set; }

    /// <summary>每页条数。</summary>
    public int PageSize { get; set; }

    /// <summary>总记录数。</summary>
    public int TotalCount { get; set; }

    /// <summary>总页数（由 <see cref="TotalCount"/> 与 <see cref="PageSize"/> 推导；<see cref="PageSize"/> ≤ 0 时为 0）。</summary>
    public int TotalPages => PageSize <= 0 ? 0 : (int)Math.Ceiling(TotalCount / (double)PageSize);

    /// <summary>是否有下一页。</summary>
    public bool HasNext => PageSize > 0 && PageIndex < TotalPages;

    /// <summary>是否有上一页。</summary>
    public bool HasPrevious => PageIndex > 1;

    /// <summary>便捷构造（无参，供序列化 / 框架绑定使用）。</summary>
    public Pagination() { }

    /// <summary>便捷构造。</summary>
    /// <param name="pageIndex">当前页码（1 基）。</param>
    /// <param name="pageSize">每页条数。</param>
    /// <param name="totalCount">总记录数。</param>
    public Pagination(int pageIndex, int pageSize, int totalCount)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        TotalCount = totalCount;
    }

    public static Pagination<T> Create<T>(IEnumerable<T> list, int totalCount) => new(list, 1, PaginationOptions.DefaultPageSize, totalCount);

    public static Pagination<T> Create<T>(IEnumerable<T> list, int pageIndex, int totalCount) => new(list, pageIndex, PaginationOptions.DefaultPageSize, totalCount);

    public static Pagination<T> Create<T>(IEnumerable<T> list, int pageIndex, int pageSize, int totalCount) => new(list, pageIndex, pageSize, totalCount);
}
