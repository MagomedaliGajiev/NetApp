using System.Net;
using System.Net.Sockets;

namespace Lection1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                var localEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345);
                listener.Blocking = true;
                listener.Bind(localEndPoint);
                listener.Listen(100);
                Console.WriteLine("Waiting for connection...");
                var socket = listener.Accept();
                Console.WriteLine("Connected!");
                listener.Close();
            }
        }
    }
}