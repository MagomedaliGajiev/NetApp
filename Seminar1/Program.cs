using System.Net;
using System.Net.Http;
using System.Net.Sockets;

namespace Seminar1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var server = new ChatServer();
            await server.Run();
        }
    }

    public class ChatServer
    {
        TcpListener listener = new TcpListener(IPAddress.Any, 55555);

        public async Task Run()
        {
            try
            {
                listener.Start();
                await Console.Out.WriteLineAsync("Запущен");

                while (true)
                {
                    var tcpClient = await listener.AcceptTcpClientAsync();
                    Console.WriteLine("Успешно подключен.");
                    Task.Run(() => ProcessClient(tcpClient));
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
        }

        public async Task ProcessClient(TcpClient client)
        {
            var reader = new StreamReader(client.GetStream());
            var writer = new StreamWriter(client.GetStream()) { AutoFlush = true };

            var message = await reader.ReadLineAsync();
            Console.WriteLine("Получено сообщение: " + message);

            // Отправить подтверждение клиенту
            await writer.WriteLineAsync("Сообщение получено: " + message);

            writer.Close();
            client.Close();
        }
    }
}