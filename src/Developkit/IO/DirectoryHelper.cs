namespace BitNebula.DevelopKit.IO;

public class DirectoryHelper
{
    #region Group One

    public static void CopyDirectory(string sourceDirectory, string targetPath)
    {
        CopyDirectory(new DirectoryInfo(sourceDirectory), targetPath, false, false);
    }

    public static void CopyDirectory(string sourceDirectory, string targetPath, bool recursive)
    {
        CopyDirectory(new DirectoryInfo(sourceDirectory), targetPath, recursive, false);
    }

    public static void CopyDirectory(string sourceDirectory, string targetPath, bool recursive, bool overwrite)
    {
        CopyDirectory(new DirectoryInfo(sourceDirectory), targetPath, recursive, overwrite);
    }

    #endregion

    #region Group Two

    public static void CopyDirectory(DirectoryInfo sourceDirectory, string targetPath)
    {
        CopyDirectory(sourceDirectory, targetPath, false, false);
    }

    public static void CopyDirectory(DirectoryInfo sourceDirectory, string targetPath, bool recursive)
    {
        CopyDirectory(sourceDirectory, targetPath, recursive, false);
    }

    public static void CopyDirectory(DirectoryInfo sourceDirectory, string targetPath, bool recursive, bool overwrite)
    {
        CopyDirectory(sourceDirectory, new DirectoryInfo(targetPath), recursive, overwrite);
    }

    #endregion

    #region Group Three

    public static void CopyDirectory(DirectoryInfo sourceDirectory, DirectoryInfo targetDirectory)
    {
        CopyDirectory(sourceDirectory, targetDirectory, false, false);
    }

    public static void CopyDirectory(DirectoryInfo sourceDirectory, DirectoryInfo targetDirectory, bool recursive)
    {
        CopyDirectory(sourceDirectory, targetDirectory, recursive, false);
    }

    public static void CopyDirectory(DirectoryInfo sourceDirectory, DirectoryInfo targetDirectory, bool recursive, bool overwrite)
    {
        // Check if the source directory exists
        if (!sourceDirectory.Exists)
        {
            throw new DirectoryNotFoundException($"Source directory not found: {sourceDirectory.FullName}");
        }

        // Create the destination directory
        if (!targetDirectory.Exists)
        {
            targetDirectory.Create();
        }

        // Get the files in the source directory and copy to the destination directory
        FileInfo[] files = sourceDirectory.GetFiles();
        foreach (FileInfo file in files)
        {
            string targetFilePath = Path.Combine(targetDirectory.FullName, file.Name);
            file.CopyTo(targetFilePath, overwrite);
        }

        // If recursive and copying subdirectories, recursively call this method
        if (recursive)
        {
            // Cache directories before we start copying
            DirectoryInfo[] dirs = sourceDirectory.GetDirectories();
            foreach (DirectoryInfo subDir in dirs)
            {
                DirectoryInfo newDirectory = targetDirectory.Combine(subDir);
                CopyDirectory(subDir, newDirectory, recursive, overwrite);
            }
        }
    }

    #endregion
}
