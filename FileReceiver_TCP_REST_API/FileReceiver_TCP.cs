using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FileReceiver_TCP_REST_API
{
    public class FileReceiver_TCP
    {


        public void Start()
        {
            // Adress IP & number port
            //IPAddress ipAddress = IPAddress.Parse("192.168.1.40");
            int port = 21136;

            // Utworzenie gniazda nasłuchującego
            TcpListener listener = new TcpListener(IPAddress.Any, port);
            listener.Start();

            // Waiting for a connection
            Console.WriteLine("Waiting for a connection...");
            TcpClient client = listener.AcceptTcpClient();
            Console.WriteLine("Connected!");


            // Receive file
            NetworkStream stream = client.GetStream();
            string fileName = "file.txt"; // Name file
            using (FileStream fileStream = File.Create(fileName))
            {
                byte[] buffer = new byte[1024];
                int bytesRead = 0;
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    fileStream.Write(buffer, 0, bytesRead);
                }
            }

            // Information about save file
            Console.WriteLine("File saved as: {0}", fileName);





            //// Odbieranie danych
            //NetworkStream stream = client.GetStream();
            //byte[] data = new byte[1024];
            //int bytesRead = stream.Read(data, 0, data.Length);
            //string message = Encoding.ASCII.GetString(data, 0, bytesRead);

            //// Wyświetlenie otrzymanych danych
            //Console.WriteLine("Received: {0}", message);





            // Zakończenie połączenia
            stream.Close();
            client.Close();
            listener.Stop();
        }

    }
}

//public TcpListener listener;
//public int port = 8000;
//public string savePath = @"C:\ReceivedFiles\";

//listener = new TcpListener(IPAddress.Any, port);
//listener.Start();
//while (true)
//{
//    Console.WriteLine("Waiting for a connection...");
//    var client = listener.AcceptTcpClient();
//    Console.WriteLine("Client connected.");
//    var stream = client.GetStream();

//    var headerBytes = new byte[1024];
//    var headerLength = stream.Read(headerBytes, 0, headerBytes.Length);
//    var header = Encoding.UTF8.GetString(headerBytes, 0, headerLength);
//    var parts = header.Split('|');
//    var fileName = parts[0];
//    var fileSize = long.Parse(parts[1]);
//    var filePath = Path.Combine(savePath, fileName);

//    using (var fileStream = new FileStream(filePath, FileMode.Create))
//    {
//        var buffer = new byte[1024];
//        int bytesReceived;
//        long bytesLeft = fileSize;
//        while (bytesLeft > 0 && (bytesReceived = stream.Read(buffer, 0, (int)Math.Min(buffer.Length, bytesLeft))) > 0)
//        {
//            fileStream.Write(buffer, 0, bytesReceived);
//            bytesLeft -= bytesReceived;
//        }
//    }

//    client.Close();
//}
