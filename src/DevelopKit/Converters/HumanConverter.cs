namespace BitNebula.DevelopKit.Converters;

public static class HumanConverter
{
    private static readonly string[] SizeUnits = ["B", "KB", "MB", "GB", "TB", "PB", "EB"];

    public static string ToReadableSize(long bytes, bool binary = false, int decimals = 2)
    {
        if (bytes < 0) throw new ArgumentOutOfRangeException(nameof(bytes), "文件大小不能为负数");

        if (decimals < 0) decimals = 0;
        if (decimals > 3) decimals = 3;

        if (bytes == 0) return "0 B";

        double divisor = binary ? 1024d : 1000d;

        double size = bytes;
        int unitIndex = 0;

        while (size >= divisor && unitIndex < SizeUnits.Length - 1)
        {
            size /= divisor;
            unitIndex++;
        }

        // 当单位为 B 时不显示小数
        string format = unitIndex == 0 ? "0" : "F" + decimals;

        return $"{size.ToString(format)} {SizeUnits[unitIndex]}";
    }
}
