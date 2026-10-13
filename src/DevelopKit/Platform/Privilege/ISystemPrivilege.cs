using System.Runtime.Versioning;

namespace BitNebula.DevelopKit.Platform.Privilege;

/// <summary>
/// 系统特权
/// </summary>
public interface ISystemPrivilege
{
    /// <summary>当前进程是否已具备管理员/特权权限。</summary>
    bool IsPrivileged { get; }

    /// <summary>
    /// 提权
    /// </summary>
    /// <param name="program"></param>
    /// <param name="args"></param>
    /// <param name="waitForExit"></param>
    /// <returns></returns>
    ElevationResult Elevation(string program, IEnumerable<string> args, bool waitForExit = false);

    [SupportedOSPlatform("windows")]
    [SupportedOSPlatform("linux")]
    static ISystemPrivilege Create()
    {
        if (OperatingSystem.IsWindows())
        {
            return new WindowsPrivilege();
        }
        else if (OperatingSystem.IsLinux())
        {
            return new LinuxPrivilege();
        }
        throw new PlatformNotSupportedException("不支持的平台");
    }
}
