using System.Text;

namespace Nebula.DevelopKit;

public class UriBuilder
{
    private const char SLASH = '/';

    private readonly StringBuilder builder;

    public UriBuilder()
    {
        builder = new StringBuilder(512);
    }

    public UriBuilder(string? uri) : this()
    {
        if (!string.IsNullOrEmpty(uri))
        {
            builder.Append(uri);
        }
    }

    public bool EndsWithSlash => builder.Length > 0 && builder[^1] == '/';

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

        string temp = uri.Replace("\\", "/");
        // TODO: 删除多余的 "//"

        if (EndsWithSlash)
        {
            if (temp.StartsWith(SLASH))
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
            if (temp.StartsWith(SLASH))
            {
                builder.Append(temp);
            }
            else
            {
                builder.Append(SLASH).Append(temp);
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
