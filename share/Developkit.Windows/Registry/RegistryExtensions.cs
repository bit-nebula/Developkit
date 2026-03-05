using System.Runtime.Versioning;
using Microsoft.Win32;

namespace Nebula.Developkit.Windows.Registry;

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

    public static string GetStringValue(this RegistryKey registryKey, string valueName, string defaultValue = "")
    {
        return registryKey.GetValue(valueName, defaultValue)?.ToString() ?? defaultValue;
    }

    public static int GetInt32Value(this RegistryKey registryKey, string valueName, int defaultValue = 0)
    {
        object? value = registryKey.GetValue(valueName, defaultValue);
        if (value is int intValue)
        {
            return intValue;
        }
        else if (value is string strValue && int.TryParse(strValue, out int parsedInt))
        {
            return parsedInt;
        }
        return defaultValue;
    }
}
