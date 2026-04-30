using System.Text;

namespace BitNebula.DevelopKit;

/// <summary>
/// Url建造器
/// </summary>
public class UrlBuilder
{
    public const char ForwardSlash = '/';
    public const char BackwardSlash = '\\';

    private readonly List<string> _pathItems;

    private bool _isFirst = true;
    private bool _hasSlashPrefix = false;

    public bool? UseSSL { get; private set; }

    public string? Host { get; private set; }

    public int? Port { get; private set; }

    [Obsolete("请使用静态工厂方法 Create() 来创建 UrlBuilder 实例", false)]
    public UrlBuilder()
    {
        _pathItems = new(12);
    }

    private UrlBuilder(int length)
    {
        _pathItems = new(length);
    }

    public static UrlBuilder Create()
    {
        return new UrlBuilder(12);
    }

    public UrlBuilder WithSSL() => WithSSL(false);

    public UrlBuilder WithSSL(bool useSSL)
    {
        UseSSL = useSSL;
        return this;
    }

    public UrlBuilder WithHost(string host)
    {
        Host = host;

        // 如果没有设置SSL，默认使用HTTP协议
        if (!UseSSL.HasValue)
        {
            UseSSL = false;
        }

        // 如果Host被设置值，说明路径有斜杠前缀
        if (!_hasSlashPrefix)
        {
            _hasSlashPrefix = true;
        }

        return this;
    }

    public UrlBuilder WithPort(int port)
    {
        Port = port;
        return this;
    }

    public UrlBuilder WithPort(string port)
    {
        if (int.TryParse(port, out int result))
        {
            Port = result;
        }
        else
        {
            throw new ArgumentException("Invalid port number", nameof(port));
        }
        return this;
    }

    public UrlBuilder WithPort(int? port)
    {
        Port = port;
        return this;
    }

    /// <summary>
    /// 确保路径有斜杠前缀
    /// </summary>
    /// <returns></returns>
    /// <remarks>适用于直接拼接路径部分的场景, 如果Host被设置值, 则默认路径有斜杠前缀</remarks>
    public UrlBuilder EnsureForwardSlash()
    {
        _hasSlashPrefix = true;
        return this;
    }

    public UrlBuilder Append(string path)
    {
        if (string.IsNullOrEmpty(path)) return this;

        if (_isFirst)
        {
            _isFirst = false;
            if (path.StartsWith(ForwardSlash) || path.StartsWith(BackwardSlash))
            {
                _hasSlashPrefix = true;
                path = path[1..];
            }
        }

        // 主要解决路径中可能存在的多个斜杠分隔符问题
        var result = ParseString(path);
        foreach (var item in result)
        {
            _pathItems.Add(item);
        }

        return this;
    }

    private static IEnumerable<string> ParseString(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            yield break;
        }

        // 用于临时拼接当前片段
        StringBuilder sb = new(24);

        foreach (char c in input)
        {
            // 判断是否是分隔符：/ 或者 \
            if (c == ForwardSlash || c == BackwardSlash)
            {
                // 只有当前片段不为空时才添加（跳过连续分隔符）
                if (sb.Length > 0)
                {
                    yield return sb.ToString();
                    sb.Clear(); // 重置临时片段
                }
            }
            else
            {
                // 非分隔符，拼接到当前片段
                sb.Append(c);
            }
        }

        // 最后把剩余的内容也加入列表（字符串末尾没有分隔符的情况）
        if (sb.Length > 0)
        {
            yield return sb.ToString();
        }
    }

    public string Build()
    {
        StringBuilder sb = new(_pathItems.Count * 16);

        // Tips: 暂时将Host节点设置为域名拼接关键设置, 如果Host没有被设置值, 则只拼接路径部分
        if (Host is not null)
        {
            // 如果设置了Host，说明需要拼接协议和Host部分
            if (UseSSL.HasValue)
            {
                sb.Append(UseSSL.Value ? "https://" : "http://");
            }

            sb.Append(Host);

            // 仅Host被设置值时, 才考虑端口号的拼接
            if (Port is not null)
            {
                sb.Append(':').Append(Port);
            }
        }

        if (_pathItems.Count > 0)
        {
            using var enumerator = _pathItems.GetEnumerator();

            // 处理第一个路径片段，考虑是否有斜杠前缀
            enumerator.MoveNext();
            if (_hasSlashPrefix)
            {
                sb.Append(ForwardSlash);
            }
            sb.Append(enumerator.Current);

            // 迭代路径片段，拼接成完整路径
            while (enumerator.MoveNext())
            {
                sb.Append(ForwardSlash).Append(enumerator.Current);
            }
        }

        return sb.ToString();
    }

    public override string ToString() => Build();
}
