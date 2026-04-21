using System.Text;

namespace BitNebula.DevelopKit.Encrypt;

public partial class EncryptUtility
{
    public static string SHA256(string content) => SHA256(content, Encoding.UTF8);

    public static string SHA256(string content, Encoding encoding)
    {
        var bytes = encoding.GetBytes(content);
        return SHA256(bytes);
    }

    /// <summary>
    /// 计算字节数组的SHA256
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static string SHA256(byte[] bytes)
    {
        byte[] by = System.Security.Cryptography.SHA256.HashData(bytes);
        return Convert.ToHexString(by);
    }

    public static string SHA256(Stream stream)
    {
        stream.Position = 0;
        using var Sha256 = System.Security.Cryptography.SHA256.Create();
        byte[] by = Sha256.ComputeHash(stream);
        return Convert.ToHexString(by);
    }
}
