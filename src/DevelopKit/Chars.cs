namespace BitNebula.DevelopKit;

/// <summary>
/// 全局通用字符常量
/// </summary>
public static class Chars
{
    /// <summary>
    /// 正斜杠 / (Unix 路径、URL、URI 分隔符)
    /// </summary>
    public const char ForwardSlash = '/';

    /// <summary>
    /// 反斜杠 \ (Windows 路径分隔符)
    /// </summary>
    public const char BackwardSlash = '\\';

    /// <summary>
    /// 点 . (文件扩展名分隔符、命名空间分隔符等)
    /// </summary>
    public const char Dot = '.';

    /// <summary>
    /// 连字符 - (常用于连接单词、参数分隔符等)
    /// </summary>
    public const char Hyphen = '-';

    /// <summary>
    /// 下划线 _ (常用于连接单词、变量命名等)
    /// </summary>
    public const char Underscore = '_';

    /// <summary>
    /// 空格   (常用于分隔单词、参数等)
    /// </summary>
    public const char Space = ' ';

    /// <summary>
    /// 逗号 , (常用于分隔列表项、参数等)
    /// </summary>
    public const char Comma = ',';

    /// <summary>
    /// 冒号 : (常用于分隔键值对、时间等)
    /// </summary>
    public const char Colon = ':';

    /// <summary>
    /// 分号 ; (常用于分隔语句、参数等)
    /// </summary>
    public const char Semicolon = ';';

    public static bool IsAsciiLettr(char c) => (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');
}
