using Microsoft.Win32;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Versioning;

namespace BitNebula.Developkit.Windows;

[SupportedOSPlatform("windows")]
public class RegistryUtil : IDisposable
{
    private bool disposedValue;
    private readonly RegistryKey rootKey;

    [AllowNull]
    private RegistryKey? currentKey;

    private RegistryUtil(RegistryHive registryHive, RegistryView? registryView)
    {
        rootKey = RegistryKey.OpenBaseKey(registryHive, registryView ?? RegistryView.Default);
    }

    public static RegistryUtil Create(RegistryHive registryHive, RegistryView? registryView = null)
    {
        registryView ??= Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32;

        RegistryUtil registry = new(registryHive, registryView);

        return registry;
    }

    /// <summary>
    /// 判断子项是否存在
    /// </summary>
    /// <param name="subkeyPath"></param>
    /// <returns></returns>
    public bool IsSubkeyExists(string subkeyPath)
    {
        RegistryKey? subkey = rootKey.OpenSubKey(subkeyPath);
        return subkey is null;
    }

    public bool TryOpenSubKey(string subkeyPath)
    {
        return TryOpenSubKey(subkeyPath, false);
    }

    public bool TryOpenSubKey(string subkeyPath, bool writable)
    {
        var temp = rootKey.OpenSubKey(subkeyPath, writable);
        if (temp is not null)
        {
            currentKey = temp;
        }
        return temp is not null;
    }

    /// <summary>
    /// 打开或创建注册表子项
    /// </summary>
    /// <param name="subkeyPath"></param>
    /// <param name="writable"></param>
    /// <returns>True: Open Subkey, False: Create Subkey</returns>
    public bool OpenOrCreateSubkey(string subkeyPath)
    {
        return OpenOrCreateSubkey(subkeyPath, false);
    }

    /// <summary>
    /// 打开或创建注册表子项
    /// </summary>
    /// <param name="subkeyPath"></param>
    /// <param name="writable"></param>
    /// <returns>True: Open Subkey, False: Create Subkey</returns>
    public bool OpenOrCreateSubkey(string subkeyPath, bool writable)
    {
        if (currentKey is null)
        {
            currentKey = rootKey.OpenSubKey(subkeyPath, writable);
        }
        else
        {
            var tempKey = currentKey.OpenSubKey(subkeyPath, writable);
            currentKey.Dispose();
            currentKey = tempKey;
        }

        if (currentKey is null)
        {
            currentKey = rootKey.CreateSubKey(subkeyPath, writable);
            return false;
        }
        return true;
    }

    public object? ReadValue(string keyName)
    {
        EnsureCurrentRegistryKey();

        return currentKey.GetValue(keyName);
    }

    public string? ReadStringValue(string keyName)
    {
        var value = ReadValue(keyName);
        return value?.ToString();
    }

    public void SetValue(string keyName, string value)
    {
        EnsureCurrentRegistryKey();

        currentKey.SetValue(keyName, value, RegistryValueKind.String);
    }

    public void DeleteValue(string keyName)
    {
        EnsureCurrentRegistryKey();

        currentKey.DeleteValue(keyName, false);
    }

    [MemberNotNull(nameof(currentKey))]
    private void EnsureCurrentRegistryKey()
    {
        if (currentKey is null)
        {
            throw new Exception("Current registry is null");
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: 释放托管状态(托管对象)
                currentKey?.Dispose();
                currentKey = null;
                rootKey.Dispose();
            }

            // TODO: 释放未托管的资源(未托管的对象)并重写终结器
            // TODO: 将大型字段设置为 null
            disposedValue = true;
        }
    }

    // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
    // ~RegistryUtil()
    // {
    //     // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
