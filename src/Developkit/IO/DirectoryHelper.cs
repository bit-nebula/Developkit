using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitNebula.Developkit.IO;

public class DirectoryHelper
{
    public static void CopyDirectory(string sourceDirectory, string targetPath, bool recursive, bool overwrite = false)
    {
        CopyDirectory(new DirectoryInfo(sourceDirectory), targetPath, recursive, overwrite);
    }

    public static void CopyDirectory(DirectoryInfo sourceDirectory, string targetPath, bool recursive, bool overwrite = false)
    {
        // Check if the source directory exists
        if (!sourceDirectory.Exists) throw new DirectoryNotFoundException($"Source directory not found: {sourceDirectory.FullName}");

        // Create the destination directory
        Directory.CreateDirectory(targetPath);

        // Get the files in the source directory and copy to the destination directory
        FileInfo[] files = sourceDirectory.GetFiles();
        foreach (FileInfo file in files)
        {
            string targetFilePath = Path.Combine(targetPath, file.Name);
            file.CopyTo(targetFilePath, overwrite: true);
        }

        // If recursive and copying subdirectories, recursively call this method
        if (recursive)
        {
            // Cache directories before we start copying
            DirectoryInfo[] dirs = sourceDirectory.GetDirectories();
            foreach (DirectoryInfo subDir in dirs)
            {
                string newDestinationDir = Path.Combine(targetPath, subDir.Name);
                CopyDirectory(subDir, newDestinationDir, recursive, overwrite);
            }
        }
    }
}
