using System.Text;

namespace BitNebula.DevelopKit;

/// <summary>
/// 文件路径建造器
/// </summary>
/// <remarks>
/// <list type="bullet">
///   <item>路径名称不允许使用不可见字符</item>
///   <item>路径名称不允许将"/"或"\"作为名称</item>
/// </list>
/// </remarks>
public class PathBuilder
{
    private readonly List<string> _parts;

    private bool _isFirst = true;
    private bool _hasSlashPrefix = false;
    private string? _rootPath;

    public PathBuilder()
    {
        _parts = new(12);
    }

    public PathBuilder HasRoot(char disk)
    {
        if (Chars.IsAsciiLettr(disk))
        {
            _rootPath = $"{disk}:\\";
            return this;
        }
        throw new ArgumentException("Disk char is not invalid!");
    }

    //public PathBuilder HasRoot(string rootPath)
    //{
    //    if (string.IsNullOrWhiteSpace(rootPath)) return this;

    //    if (Chars.IsAsciiLettr(rootPath[0]) && rootPath.Length == 1 || rootPath[1] == ':')
    //    {
    //        if (rootPath.Length > 1)
    //        {
    //        }

    //        foreach (char c in rootPath)
    //        {
    //        }
    //        if (rootPath.StartsWith("\\\\"))
    //        {

    //        }
    //    }
    //}

    public PathBuilder Append(string segment)
    {
        if (string.IsNullOrWhiteSpace(segment)) return this;

        if (_isFirst)
        {
            _isFirst = false;
            _hasSlashPrefix = true;
            if (segment.StartsWith(Chars.ForwardSlash) || segment.StartsWith(Chars.BackwardSlash))
            {
                segment = segment[1..];
            }
        }

        var items = ParseSegment(segment);
        foreach (var item in items)
        {
            _parts.Add(item);
        }

        return this;
    }

    public PathBuilder Append(IEnumerable<string> segments)
    {
        foreach (var segment in segments)
        {
            Append(segment);
        }
        return this;
    }

    private static IEnumerable<string> ParseSegment(string segment)
    {
        if (string.IsNullOrWhiteSpace(segment)) yield break;

        var sb = new StringBuilder(segment.Length);

        foreach (var c in segment)
        {
            // TODO: 可以考虑添加更多的非法字符检查（如 Windows 文件系统中的 < > | ? * 等）
            // TODO: 字符中间的空格是允许的，(Windows)结尾的空格需要删除


            //// 跳过不可见字符
            //if (char.IsControl(c) || char.IsWhiteSpace(c)) continue;

            // Tips: 暂不考虑非法路径字符

            // 若当前字符是分隔符时
            if (c == Chars.ForwardSlash || c == Chars.BackwardSlash)
            {
                // 只有当前片段不为空时才添加（跳过连续分隔符）
                if (sb.Length > 0)
                {
                    yield return sb.ToString();
                    sb.Clear(); // 重置临时片段
                }
            }
            // 若当前字符不是分隔符, 则添加到当前片段
            else
            {
                sb.Append(c);
            }
        }

        // 最后把剩余的内容也加入列表（字符串末尾没有分隔符的情况）
        if (sb.Length > 0)
        {
            yield return sb.ToString();
        }
    }

    private int GetLength()
    {
        int length = 0;

        if (_rootPath is not null)
        {
            length = _rootPath.Length;
        }
        else if (_hasSlashPrefix)
        {
            length = 1;
        }

        if (_parts.Count > 0)
        {
            length += _parts.Sum(x => x.Length) + _parts.Count - 1;
        }

        return length;
    }

    public string Build()
    {
        // Tips: 仅考虑Windows系统环境

        int length = GetLength();

        StringBuilder sb = new(length);

        if (_rootPath is not null)
        {
            sb.Append(_rootPath);
        }

        if (_parts.Count > 0)
        {
            using var enumerator = _parts.GetEnumerator();

            // 处理第一个路径片段，考虑是否有斜杠前缀
            enumerator.MoveNext();
            if (_hasSlashPrefix && _rootPath is null)
            {
                sb.Append(Chars.BackwardSlash);
            }
            sb.Append(enumerator.Current);

            // 迭代路径片段，拼接成完整路径
            while (enumerator.MoveNext())
            {
                sb.Append(Chars.BackwardSlash).Append(enumerator.Current);
            }
        }

        return sb.ToString();
    }

    public override string ToString() => Build();
}
