using System.Runtime.Versioning;
using BitNebula.Developkit.Windows;
using Microsoft.Win32;

namespace Nebula.Developkit.Windows.Test;

[TestClass]
public sealed class RegistryUtilTest
{
    [TestMethod]
    [SupportedOSPlatform("windows")]
    public void TestMethod1()
    {
        using var reg = RegistryUtil.Create(RegistryHive.CurrentUser);
        Assert.IsNotNull(reg);

    }
}
