namespace Nebula.Developkit.IO;

public static class DirectoryInfoExtension
{
    public static void CopyTo(this DirectoryInfo sourceDirectory, string target, bool recursive)
    {
        CopyTo(sourceDirectory, target, recursive, false);
    }

    public static void CopyTo(this DirectoryInfo sourceDirectory, string target, bool recursive, bool overwrite)
    {
        var targetDirectory = new DirectoryInfo(target);
        CopyTo(sourceDirectory, targetDirectory, recursive, overwrite);
    }

    public static void CopyTo(this DirectoryInfo sourceDirectory, DirectoryInfo target, bool recursive)
    {
        CopyTo(sourceDirectory, target, recursive, false);
    }

    public static void CopyTo(this DirectoryInfo sourceDirectory, DirectoryInfo target, bool recursive, bool overwrite)
    {
        if (!sourceDirectory.Exists) throw new DirectoryNotFoundException($"Source directory '{sourceDirectory.FullName}' does not exist.");

        if (!target.Exists) target.Create();

        FileInfo[] files = sourceDirectory.GetFiles();
        foreach (FileInfo file in files)
        {
            var targetFilePath = Path.Combine(target.FullName, file.Name);
            file.CopyTo(targetFilePath, overwrite);
        }

        if (recursive)
        {
            DirectoryInfo[] directories = sourceDirectory.GetDirectories();
            foreach (DirectoryInfo directory in directories)
            {
                directory.CopyTo(Path.Combine(target.FullName, directory.Name), recursive, overwrite);
            }
        }
    }
}
