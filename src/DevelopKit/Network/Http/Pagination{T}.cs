namespace BitNebula.DevelopKit.Network.Http;

/// <summary>
/// 分页结果：携带本页数据列表与分页元数据（<see cref="Pagination"/>）。
/// 通常作为 <see cref="HttpResult{T}"/> 的 <c>T</c> 使用，例如 <c>HttpResult&lt;Pagination&lt;OrderDto&gt;&gt;</c>，
/// 序列化后所有字段（含分页元数据）平铺在 <c>data</c> 内：<c>{ "code":100, "message":"Success", "data":{ "items":[...], "pageIndex":1, "pageSize":20, "totalCount":135, "totalPages":7, "hasNext":true, "hasPrevious":false }, "status":true }</c>。
/// </summary>
/// <typeparam name="T">本页数据项类型。</typeparam>
public class Pagination<T> : Pagination
{
    /// <summary>本页数据。</summary>
    public IEnumerable<T> Items { get; set; } = Array.Empty<T>();

    public Pagination() { }

    //public Pagination(IEnumerable<T> items, int totalCount)
    //{
    //    base.TotalCount = totalCount;
    //    Items = items;
    //}

    //public Pagination(IEnumerable<T> items, int pageIndex, int totalCount) : base(pageIndex, PaginationOptions.DefaultPageSize, totalCount)
    //{
    //    Items = items;
    //}

    public Pagination(IEnumerable<T> items, int pageIndex, int pageSize, int totalCount) : base(pageIndex, pageSize, totalCount)
    {
        Items = items;
    }
}
