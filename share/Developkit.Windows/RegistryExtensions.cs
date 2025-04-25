using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Versioning;
using System.Text;
using Microsoft.Win32;

namespace Developkit.Windows;

[SupportedOSPlatform("windows")]
public static class RegistryExtension
{
    /// <summary>
    /// 打开或创建注册表子项
    /// </summary>
    /// <param name="subkeyPath"></param>
    /// <param name="writable"></param>
    /// <returns>True: Open Subkey, False: Create Subkey</returns>
    public static RegistryKey OpenOrCreateSubkey(this RegistryKey registryKey, string subkeyPath, bool writable)
    {
        RegistryKey? currentKey = registryKey.OpenSubKey(subkeyPath, writable);
        currentKey ??= registryKey.CreateSubKey(subkeyPath, writable);
        return currentKey;
    }
}
