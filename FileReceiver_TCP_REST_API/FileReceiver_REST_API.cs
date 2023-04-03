using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace FileReceiver_TCP_REST_API
{
    public class FileReceiver_REST_API
    {
        public static async Task Main()
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://localhost:5000")
                .Configure(app =>
                {
                    app.Run(async context =>
                    {
                        if (context.Request.Method.Equals("POST") &&
                            context.Request.HasFormContentType &&
                            context.Request.Form.Files.Count > 0)
                        {
                            var file = context.Request.Form.Files[0];
                            var fileName = file.FileName;
                            var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                            }

                            context.Response.StatusCode = 200;
                            await context.Response.WriteAsync("Plik został odebrany i zapisany na dysku.");
                        }
                        else
                        {
                            context.Response.StatusCode = 400;
                            await context.Response.WriteAsync("Niepoprawne żądanie.");
                        }
                    });
                })
                .Build();

            host.Start();

            Console.WriteLine("Aplikacja konsolowa działa w trybie ciągłym. Naciśnij dowolny klawisz, aby zakończyć.");

            Console.ReadLine();

            await host.StopAsync();
        }
    }

    
}
        //public static void Start()
        //{

        //    if (!IsAdministrator())
        //    {
        //        RestartAsAdministrator();
        //        return;
        //    }
           
        //}
        //static bool IsAdministrator()
        //{
        //    var identity = WindowsIdentity.GetCurrent();
        //    var principal = new WindowsPrincipal(identity);
        //    return principal.IsInRole(WindowsBuiltInRole.Administrator);
        //}

        //static void RestartAsAdministrator()
        //{
        //    var startInfo = new ProcessStartInfo();
        //    startInfo.FileName = Assembly.GetEntryAssembly().Location;
        //    startInfo.Verb = "runas";
        //    startInfo.Arguments = string.Join(" ", Environment.GetCommandLineArgs().Skip(1));
        //    try
        //    {
        //        EXE();

        //        Process.Start(startInfo);


        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Failed to restart as administrator: " + ex.Message);
        //    }
        //    Environment.Exit(0);
        //}

        //public static void EXE()
        //{
        //    var listener = new HttpListener();
        //    listener.Prefixes.Add("http://192.168.1.40:21136/");

        //    listener.Start();

        //    Console.WriteLine("Server is running...");

        //    while (true)
        //    {
        //        var context = listener.GetContext();
        //        Task.Run(() => ProcessRequest(context));
        //    }
        //}

           
        

        //static async Task ProcessRequest(HttpListenerContext context)
        //{
        //    if (context.Request.HttpMethod == "POST")
        //    {
        //        var request = context.Request;

        //        var content = new MemoryStream();
        //        await request.InputStream.CopyToAsync(content);

        //        var fileName = request.Headers["filename"];
        //        var savePath = Path.Combine("C:\\Temp\\", fileName);
        //        File.WriteAllBytes(savePath, content.ToArray());

        //        Console.WriteLine($"File {fileName} saved at {savePath}");
        //    }

        //    context.Response.Close();
        //}

    

