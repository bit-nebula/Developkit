using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Versioning;
using System.Security.Principal;

namespace BitNebula.DevelopKit.Platform.Privilege;

/// <summary>
/// Windows 系统特权
/// </summary>
[SupportedOSPlatform("windows")]
public class WindowsPrivilege : ISystemPrivilege
{
    private bool? _IsPrivileged;
    public bool IsPrivileged
    {
        get
        {
            if (!_IsPrivileged.HasValue)
            {
                using var identity = WindowsIdentity.GetCurrent();
                var principal = new WindowsPrincipal(identity);
                _IsPrivileged = principal.IsInRole(WindowsBuiltInRole.Administrator);

            }
            return _IsPrivileged.Value;
        }
    }

    public ElevationResult Elevation(string fileName, IEnumerable<string> args, bool waitForExit = false)
    {
        var psi = new ProcessStartInfo
        {
            FileName = fileName,
            UseShellExecute = true, // 必须为 true 才能用 Verb
            Verb = "runas",         // 触发 UAC
        };

        foreach (string item in args)
        {
            psi.ArgumentList.Add(item);
        }

        try
        {
            var proc = Process.Start(psi);
            if (proc is null)
            {
                return new ElevationResult(false, ElevationErrorType.Unknown, "Process.Start 返回 null");
            }

            if (waitForExit)
            {
                proc.WaitForExit();
            }

            return new ElevationResult(true, ElevationErrorType.None);
        }
        catch (Win32Exception ex) when (ex.NativeErrorCode == 1223)
        {
            return new ElevationResult(false, ElevationErrorType.Cancelled, "用户取消了 UAC 提权");
        }
        catch (Win32Exception ex)
        {
            return new ElevationResult(false, ElevationErrorType.Denied, ex.Message);
        }
        catch (Exception ex)
        {
            return new ElevationResult(false, ElevationErrorType.Unknown, ex.Message);
        }
    }
}
