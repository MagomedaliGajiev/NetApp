using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                var remoteEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345);
   
                Console.WriteLine("Connecting...");

                try
                {
                    client.Connect(remoteEndPoint);
                }
                catch 
                {

                    
                }

                if (client.Connected)
                {
                    Console.WriteLine("Connected!");
                    Console.WriteLine($"localEndPoint = {client.LocalEndPoint}");
                    Console.WriteLine($"remoteEndPoint = {client.RemoteEndPoint}");
                }
                else
                {
                    Console.WriteLine("Connections problem!");
                    return;
                }

                byte[] bytes = Encoding.UTF8.GetBytes("Hello!");

                Console.WriteLine("Press any key for send...");
                Console.ReadKey();

                int count = client.Send(bytes);
                if (count == bytes.Length)
                {
                    Console.WriteLine("Sended!");
                }
                else
                {
                    Console.WriteLine("Something went wrong!");
                }
            }
        }
    }
}