namespace BitNebula.DevelopKit.Network.Http;

/// <summary>
/// 分页全局选项。建议在应用启动时设置一次，例如：PaginationOptions.DefaultPageSize = 20;
/// </summary>
public static class PaginationOptions
{
    [Obsolete("不支持的配置项, 默认值为 1")]
    public static int DefaultPageIndex { get; set; } = 1;

    /// <summary>全局默认每页条数。默认 10，建议仅在程序启动时设置一次。</summary>
    public static int DefaultPageSize { get; set; } = 10;
}
