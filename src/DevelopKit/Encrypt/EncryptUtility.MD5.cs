using System.Security.Cryptography;
using System.Text;

namespace BitNebula.DevelopKit.Encrypt;

public partial class EncryptUtility
{
    /// <summary>
    /// 计算MD5
    /// </summary>
    /// <param name="content">字符串内容</param>
    /// <returns></returns>
    public static string CalcMd5(string content)
    {
        var bytes = Encoding.UTF8.GetBytes(content);
        return CalcMd5(bytes);
    }

    /// <summary>
    /// 计算MD5
    /// </summary>
    /// <remarks>字节数组重载</remarks>
    /// <param name="bytes">字节数组内容</param>
    /// <returns></returns>
    public static string CalcMd5(byte[] bytes)
    {
        //using MD5 md5 = MD5.Create();
        //var hash = md5.ComputeHash(bytes);
        var hash = MD5.HashData(bytes);
        var strResult = BitConverter.ToString(hash);
        string md5_value = strResult.Replace("-", "").ToLower();
        return md5_value;
    }

    /// <summary>
    /// 计算双重MD5
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    public static string CalcDoubleMd5(string content)
    {
        var double_md5 = CalcMd5(CalcMd5(content));
        return double_md5;
    }

    /// <summary>
    /// 计算双重MD5
    /// </summary>
    /// <remarks>字节数组重载</remarks>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static string CalcDoubleMd5(byte[] bytes)
    {
        var double_md5 = CalcMd5(CalcMd5(bytes));
        return double_md5;
    }
}
