namespace BitNebula.DevelopKit.Test;

[TestClass]
public class PathBuilder_Test
{
    [TestMethod]
    [DataRow(null, null, "folder", "\\folder")]
    [DataRow(null, null, "folder/", "\\folder")]
    [DataRow(null, null, "/folder", "\\folder")]
    [DataRow(null, null, "/folder/", "\\folder")]
    [DataRow("C", null, "folder", "C:\\folder")]
    [DataRow("C", null, "/folder", "C:\\folder")]
    [DataRow("C", null, "folder/", "C:\\folder")]
    [DataRow("C", null, "/folder/", "C:\\folder")]
    public void PathTest(string? root, string? p1, string? p2, string target)
    {
        PathBuilder builder = new();

        if (root is not null)
        {
            if (root.Length == 1)
            {
                builder.HasRoot(root[0]);
            }
            else
            {
                //builder.HasRoot(root);
            }
        }

        if (p1 is not null)
        {
            builder.Append(p1);
        }

        if (p2 is not null)
        {
            builder.Append(p2);
        }

        string result = builder.Build();
        Assert.AreEqual(target, result);
    }
}
