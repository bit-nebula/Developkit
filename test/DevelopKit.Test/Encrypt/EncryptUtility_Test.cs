using BitNebula.DevelopKit.Encrypt;

namespace BitNebula.DevelopKit.Test.Encrypt;

[TestClass]
public class EncryptUtility_Test
{
    [TestMethod]
    [DataRow("Hello, World!", "DFFD6021BB2BD5B0AF676290809EC3A53191DD81C7F70A4B28688A362182986F")]
    public void SHA256_Test(string content, string expected)
    {
        var sha256 = EncryptUtility.SHA256(content);
        Assert.AreEqual(expected, sha256);
    }
}
