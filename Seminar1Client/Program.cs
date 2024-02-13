using System.Net.Sockets;

namespace SeminarClient1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var tcpClient = new TcpClient();
            tcpClient.Connect("localhost", 55555);

            var writer = new StreamWriter(tcpClient.GetStream()) { AutoFlush = true };
            var reader = new StreamReader(tcpClient.GetStream());

            Console.WriteLine("Введите сообщение: ");
            var message = Console.ReadLine();

            // Отправить сообщение на сервер
            await writer.WriteLineAsync(message);

            // Дождаться подтверждения от сервера
            var confirmationMessage = await reader.ReadLineAsync();
            Console.WriteLine("Подтверждение: " + confirmationMessage);

            writer.Close();
            reader.Close();
            tcpClient.Close();

            Console.ReadLine();
        }
    }
}
