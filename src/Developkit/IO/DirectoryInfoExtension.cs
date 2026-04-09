namespace BitNebula.DevelopKit.IO;

public static class DirectoryInfoExtension
{
    #region MergeTo Group One

    public static void MergeTo(this DirectoryInfo sourceDirectory, string targetPath)
    {
        MergeTo(sourceDirectory, targetPath, false, false);
    }

    public static void MergeTo(this DirectoryInfo sourceDirectory, string targetPath, bool recursive)
    {
        MergeTo(sourceDirectory, targetPath, recursive, false);
    }

    public static void MergeTo(this DirectoryInfo sourceDirectory, string targetPath, bool recursive, bool overwrite)
    {
        var targetDirectory = new DirectoryInfo(targetPath);
        MergeTo(sourceDirectory, targetDirectory, recursive, overwrite);
    }

    #endregion

    #region MergeTo Group Two

    public static void MergeTo(this DirectoryInfo sourceDirectory, DirectoryInfo targetDirectory)
    {
        MergeTo(sourceDirectory, targetDirectory, false, false);
    }

    public static void MergeTo(this DirectoryInfo sourceDirectory, DirectoryInfo targetDirectory, bool recursive)
    {
        MergeTo(sourceDirectory, targetDirectory, recursive, false);
    }

    public static void MergeTo(this DirectoryInfo sourceDirectory, DirectoryInfo targetDirectory, bool recursive, bool overwrite)
    {
        if (!sourceDirectory.Exists)
        {
            throw new DirectoryNotFoundException($"Source directory '{sourceDirectory.FullName}' does not exist.");
        }

        if (!targetDirectory.Exists)
        {
            targetDirectory.Create();
        }

        FileInfo[] files = sourceDirectory.GetFiles();
        foreach (FileInfo file in files)
        {
            var targetFilePath = Path.Combine(targetDirectory.FullName, file.Name);
            file.CopyTo(targetFilePath, overwrite);
        }

        if (recursive)
        {
            DirectoryInfo[] directories = sourceDirectory.GetDirectories();
            foreach (DirectoryInfo directory in directories)
            {
                DirectoryInfo newTargetDirectory = targetDirectory.Combine(directory);
                directory.MergeTo(newTargetDirectory, recursive, overwrite);
            }
        }
    }

    #endregion

    #region Combine Group

    public static DirectoryInfo Combine(this DirectoryInfo sourceDirectory, string relativePath)
    {
        string path = Path.Combine(sourceDirectory.FullName, relativePath);
        return new DirectoryInfo(path);
    }

    public static DirectoryInfo Combine(this DirectoryInfo sourceDirectory, DirectoryInfo targetDirectory)
    {
        string path = Path.Combine(sourceDirectory.FullName, targetDirectory.Name);
        return new DirectoryInfo(path);
    }

    #endregion
}
