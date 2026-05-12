using System.Text;

namespace BitNebula.DevelopKit;

/// <summary>
/// Url建造器
/// </summary>
public class UrlBuilder
{
    private readonly List<string> _parts;

    private bool _isFirst = true;
    private bool _hasSlashPrefix = false;

    public bool? UseSSL { get; private set; }

    public string? Host { get; private set; }

    public int? Port { get; private set; }

    private UrlBuilder() : this(12)
    {

    }

    private UrlBuilder(int length)
    {
        _parts = new(length);
    }

    public static UrlBuilder Create()
    {
        return new UrlBuilder(12);
    }

    public static UrlBuilder Create(string baseUrl)
    {
        var instance = new UrlBuilder(12);

        if (string.IsNullOrWhiteSpace(baseUrl)) return instance;

        // 兼容相对路径的情况
        if (baseUrl[0].Equals(Chars.ForwardSlash) || baseUrl[0].Equals(Chars.BackwardSlash))
        {
            return instance.Append(baseUrl);
        }

        //baseUrl = baseUrl.Replace("\\", "/");
        int schemeIndex = baseUrl.IndexOf("://") + 3;

        if (schemeIndex == baseUrl.Length) return instance;

        // 解析 Scheme
        string scheme = baseUrl[..schemeIndex];
        if (schemeIndex == UrlSchemes.Http.Length && scheme.Equals(UrlSchemes.Http, StringComparison.OrdinalIgnoreCase))
        {
            instance.UseSSL = false;
        }
        else if (schemeIndex == UrlSchemes.Https.Length && scheme.Equals(UrlSchemes.Https, StringComparison.OrdinalIgnoreCase))
        {
            instance.UseSSL = true;
        }

        // 解析 Host
        int hostIndex = 0;
        if (instance.UseSSL.HasValue)
        {
            hostIndex = baseUrl.IndexOf(Chars.ForwardSlash, schemeIndex);

            string host;
            if (hostIndex == -1)
            {
                host = baseUrl[schemeIndex..];
            }
            else
            {
                host = baseUrl[schemeIndex..hostIndex];
            }

            // 解析 Port
            int portIndex = host.IndexOf(Chars.Colon);
            if (portIndex == -1)
            {
                instance.Host = host;
            }
            else
            {
                instance.Host = host[..portIndex];

                if (int.TryParse(host[(portIndex + 1)..], out int port))
                {
                    instance.Port = port;
                }
            }
        }

        // 解析路径
        if (hostIndex != -1 && hostIndex + 1 < baseUrl.Length)
        {
            int queryIndex = baseUrl.IndexOf('?', hostIndex);
            if(queryIndex == -1)
            {
                instance.Append(baseUrl[hostIndex..]);
            }
            else
            {
                instance.Append(baseUrl[hostIndex..queryIndex]);
            }
        }

        return instance;
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

    public UrlBuilder Append(string segment)
    {
        if (string.IsNullOrWhiteSpace(segment)) return this;

        if (_isFirst)
        {
            _isFirst = false;
            if (segment.StartsWith(Chars.ForwardSlash) || segment.StartsWith(Chars.BackwardSlash))
            {
                _hasSlashPrefix = true;
                segment = segment[1..];
            }
            else if(Host is not null)
            {
                _hasSlashPrefix = true;
            }
        }

        // 主要解决路径中可能存在的多个斜杠分隔符问题
        var items = ParseSegment(segment);
        foreach (var item in items)
        {
            _parts.Add(item);
        }

        return this;
    }

    public UrlBuilder Append(IEnumerable<string> segments)
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

        // 用于临时拼接当前片段
        StringBuilder sb = new(segment.Length);

        foreach (char c in segment)
        {
            // 跳过不可见字符
            if (char.IsControl(c) || char.IsWhiteSpace(c)) continue;

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
        int length = _parts.Sum(x => x.Length);
        if(_parts.Count > 0)
        {
            length += _parts.Count - 1;
        }
        if (Host is not null)
        {
            length += Host.Length;
            if (UseSSL.HasValue)
            {
                length += UseSSL.Value ? UrlSchemes.Https.Length : UrlSchemes.Http.Length;
            }
            if (Port.HasValue)
            {
                length += 1 + Port.Value.ToString().Length;
            }
        }
        if (_hasSlashPrefix)
        {
            length += 1;
        }
        return length;
    }

    public string Build()
    {
        int length = GetLength();

        StringBuilder sb = new(length + 1); // Tips: 申请的空间+1

        // Tips: 暂时将Host节点设置为域名拼接关键设置, 如果Host没有被设置值, 则只拼接路径部分
        if (Host is not null)
        {
            // 如果设置了Host，说明需要拼接协议和Host部分
            if (UseSSL.HasValue)
            {
                sb.Append(UseSSL.Value ? UrlSchemes.Https : UrlSchemes.Http);
            }

            sb.Append(Host);

            // 仅Host被设置值时, 才考虑端口号的拼接
            if (Port is not null)
            {
                sb.Append(Chars.Colon).Append(Port);
            }
        }

        if (_parts.Count > 0)
        {
            using var enumerator = _parts.GetEnumerator();

            // 处理第一个路径片段，考虑是否有斜杠前缀
            enumerator.MoveNext();
            if (_hasSlashPrefix)
            {
                sb.Append(Chars.ForwardSlash);
            }
            sb.Append(enumerator.Current);

            // 迭代路径片段，拼接成完整路径
            while (enumerator.MoveNext())
            {
                sb.Append(Chars.ForwardSlash).Append(enumerator.Current);
            }
        }
#if DEBUG
        if (length != sb.Length)
        {
            throw new Exception("leng not trues");
        }
#endif
        return sb.ToString();
    }

    public override string ToString() => Build();
}
