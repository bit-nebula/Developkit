namespace BitNebula.DevelopKit.Platform.Privilege;

public class LinuxPrivilege : ISystemPrivilege
{
    public bool IsPrivileged => throw new NotImplementedException();

    public bool CheckElevated()
    {
        throw new NotImplementedException();
    }

    public ElevationResult Elevation(string program, IEnumerable<string> args, bool waitForExit = false)
    {
        throw new NotImplementedException();
    }
}
