using System.Text;

namespace BitNebula.DevelopKit.Encrypt;

public partial class EncryptUtility
{
    private const string Symbol = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

    /// <summary>
    /// 生成随机字符串
    /// </summary>
    /// <remarks>默认返回6个随机字符串</remarks>
    /// <returns></returns>
    public static string Noncestr(int n = 6)
    {
        var sb = new StringBuilder(n);
        var rd = new Random();
        for (int i = 0; i < n; i++)
        {
            sb.Append(Symbol.AsSpan(rd.Next(0, Symbol.Length), 1));
        }
        return sb.ToString();
    }

    /// <summary>
    /// 计算磁盘大小字符串
    /// </summary>
    /// <param name="size"></param>
    /// <returns></returns>
    public static string CalcDiskSizeString(long size)
    {
        string result;
        if (size < 1000000000)
        {
            result = string.Format("{0} MB", size / 1000000);
        }
        else
        {
            result = string.Format("{0:F2} GB", size / 1000000000.0);
        }
        return result;
    }
}
