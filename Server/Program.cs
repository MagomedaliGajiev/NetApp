using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                var localEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345);
                listener.Blocking = false;

                Console.WriteLine($"Socket is bound {listener.IsBound}");
                listener.Bind(localEndPoint);
                Console.WriteLine($"Socket is bound {listener.IsBound}");

                listener.Listen(100);
                Console.WriteLine("Waiting for connection...");

                Socket? socket = null;

                do
                {
                    try
                    {
                        socket = listener.Accept();
                    }
                    catch
                    {

                        Console.Write(".");
                        Thread.Sleep(1000);
                    }
                }
                while (socket == null); 


                Console.WriteLine("Connected!");
                byte[] buffer = new byte[255];

                while (socket.Available == 0) ;
                Console.WriteLine($"Available {socket.Available} bytes for reading.");

                int count = socket.Receive(buffer);

                if (count > 0)
                {
                    string message = Encoding.UTF8.GetString(buffer);
                    Console.WriteLine(message);

                }
                else
                {
                    Console.WriteLine("Message not received!");
                }

                listener.Close();
            }
        }
    }
}