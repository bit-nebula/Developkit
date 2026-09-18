using System.Text;

namespace BitNebula.DevelopKit.Encrypt;

public partial class EncryptUtility
{
    public static string HMACSHA256(string key, string content)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);
        byte[] contentBytes = Encoding.UTF8.GetBytes(content);

        byte[] hashBytes = System.Security.Cryptography.HMACSHA256.HashData(keyBytes, contentBytes);
        return Convert.ToHexString(hashBytes);
    }
}
