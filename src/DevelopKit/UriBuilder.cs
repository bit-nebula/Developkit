using System.Text;

namespace BitNebula.DevelopKit;

public class UriBuilderOptions
{
    public string Scheme { get; set; } = string.Empty;

}

public class UriScheme
{
    public string Scheme { get; init; }
    public string Value { get; init; }

    private UriScheme(string scheme, string value)
    {
        Scheme = scheme;
        Value = value;
    }

    public static UriScheme File { get; } = new UriScheme("file", "file:///");
    public static UriScheme Http { get; } = new UriScheme("http", "http://");
    public static UriScheme Https { get; } = new UriScheme("https", "https://");
}

public enum SeparatorStyle
{
    Path,
    Url,
}

public class UriBuilder
{
    public const char Forward_Slash = '/';
    public const char Backward_Slash = '\\';

    /// <summary>
    /// 分割器
    /// </summary>
    public char Separator { get; }

    public string Scheme { get; set; } = string.Empty;

    private readonly StringBuilder builder;

    public UriBuilder()
    {
        builder = new StringBuilder(255);



        if (OperatingSystem.IsWindows())
        {
            Separator = Backward_Slash;
        }
    }

    public UriBuilder(UriScheme scheme) : this()
    {
        Scheme = scheme.Value;

        builder.Append(Scheme);
    }

    public UriBuilder(string path) : this()
    {
        builder.Append(path);
    }

    public bool EndsWithSlash => builder.Length > 0 && builder[^1] == Forward_Slash;

    public UriBuilder AppendSlash()
    {
        if (!EndsWithSlash)
        {
            builder.Append('/');
        }
        return this;
    }

    public UriBuilder Append(string? uri)
    {
        if (string.IsNullOrEmpty(uri)) return this;

        string temp = uri.Replace(Backward_Slash, Forward_Slash);
        // TODO: 删除多余的 "//"

        if (EndsWithSlash)
        {
            if (temp.StartsWith(Forward_Slash))
            {
                builder.Append(temp[1..]);
            }
            else
            {
                builder.Append(temp);
            }
        }
        else
        {
            if (temp.StartsWith(Forward_Slash))
            {
                builder.Append(temp);
            }
            else
            {
                builder.Append(Forward_Slash).Append(temp);
            }
        }

        return this;
    }

    public string Build()
    {
        return builder.ToString();
    }

    public override string ToString()
    {
        return builder.ToString();
    }
}
