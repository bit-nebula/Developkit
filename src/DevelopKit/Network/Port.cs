using System.Net;
using System.Net.Sockets;

namespace BitNebula.DevelopKit.Network;

public partial class Port
{
    public const int MinPort = 0;
    public const int MaxPort = 65535;

    /// <summary>
    /// 校验端口号是否在合法范围内（0-65535）
    /// </summary>
    /// <param name="port">端口号</param>
    /// <returns>合法返回 true，否则 false</returns>
    public static bool IsValid(int port) => port >= MinPort && port <= MaxPort;

    #region Tcp Port

    public static bool TcpPortAvailable(int port) => TcpPortAvailable(IPAddress.Loopback, port);

    /// <summary>
    /// TCP协议下的端口是否可用
    /// </summary>
    /// <param name="port"></param>
    /// <param name="address"></param>
    /// <returns></returns>
    public static bool TcpPortAvailable(IPAddress address, int port) => TcpPortAvailable(new IPEndPoint(address, port));

    public static bool TcpPortAvailable(IPEndPoint endPoint) => Available(endPoint, SocketType.Stream, ProtocolType.Tcp);

    [Obsolete("即将移除此API")]
    public static bool TcpPortAvailable(int port, string host)
    {
        if (!IPAddress.TryParse(host, out IPAddress? address))
        {
            throw new ArgumentException($"Param address is invalid: {host}");
        }
        return Available(address, port, SocketType.Stream, ProtocolType.Tcp);
    }

    #endregion

    #region Udp Port

    public static bool UdpPortAvailable(int port) => UdpPortAvailable(IPAddress.Loopback, port);

    public static bool UdpPortAvailable(IPAddress address, int port) => UdpPortAvailable(new IPEndPoint(address, port));

    public static bool UdpPortAvailable(IPEndPoint endPoint) => Available(endPoint, SocketType.Dgram, ProtocolType.Udp);

    #endregion

    public static bool Available(IPAddress address, int port, SocketType socketType, ProtocolType protocolType)
    {
        return Available(new IPEndPoint(address, port), socketType, protocolType);
    }

    public static bool Available(IPEndPoint endPoint, SocketType socketType, ProtocolType protocolType)
    {
        if (!IsValid(endPoint.Port)) return false;

        try
        {
            using (Socket socket = new(endPoint.Address.AddressFamily, socketType, protocolType))
            {
                socket.ExclusiveAddressUse = true;
                socket.Bind(endPoint);
            }
            return true;
        }
        catch (SocketException)
        {
            return false;  // 绑定失败，端口已被占用或不可用
        }
    }
}
