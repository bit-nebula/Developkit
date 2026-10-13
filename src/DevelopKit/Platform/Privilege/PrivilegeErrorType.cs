namespace BitNebula.DevelopKit.Platform.Privilege;

/// <summary>
/// 提权错误类型
/// </summary>
public enum ElevationErrorType
{
    None,
    Denied,       // 用户拒绝 / 密码错误
    Cancelled,    // 用户取消
    NotFound,     // 提权器或程序不存在
    NotSupported, // 当前环境不支持
    Unknown
}
