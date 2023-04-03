using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;

namespace FileReceiver_TCP_REST_API
{
    public class TESTOWA
    {
        public static async Task Main()
        {
            string url = "http://192.168.1.40:21136/api/message"; // adres URL do nasłuchiwania

            var cts = new CancellationTokenSource();
            var listenerTask = Task.Run(async () =>
            {
                var listener = new HttpListener();
                listener.Prefixes.Add(url + "/");
                listener.Start();
                Console.WriteLine("Oczekiwanie na wiadomość...");

                while (!cts.IsCancellationRequested)
                {
                    var context = await listener.GetContextAsync();
                    if (context.Request.HttpMethod == "POST" && context.Request.Url.AbsolutePath == "/api/message")
                    {
                        var requestString = await new System.IO.StreamReader(context.Request.InputStream).ReadToEndAsync();
                        Console.WriteLine("Odebrano wiadomość: " + requestString);

                        string responseString = "Odebrano wiadomość: " + requestString;
                        context.Response.StatusCode = 200;
                        context.Response.ContentType = "text/plain";
                        var buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                        context.Response.ContentLength64 = buffer.Length;
                        var output = context.Response.OutputStream;
                        await output.WriteAsync(buffer, 0, buffer.Length);
                        output.Close();
                    }
                }
            });

            Console.WriteLine("Naciśnij dowolny klawisz, aby zakończyć nasłuchiwanie.");
            Console.ReadKey();
            cts.Cancel();
            await listenerTask;
        }
    }
}
