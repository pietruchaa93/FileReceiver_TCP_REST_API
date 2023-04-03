// See https://aka.ms/new-console-template for more information


using System.Net.Sockets;
using System.Net;
using System.Text;
using FileReceiver_TCP_REST_API;
using System.Runtime.CompilerServices;


Console.WriteLine("Open connect... Choose 1 for TCP or 2 for REST API");

string input = Console.ReadLine();

if (input == "1")
{
    Console.WriteLine("Connect via TCP");
    Console.WriteLine("Open connect. Press any button");
    Console.ReadKey();
    // code for option 1

    var option1 = new FileReceiver_TCP();
    option1.Start();

}
else if (input == "2")
{
    Console.WriteLine("Connect via REST API");
    Console.WriteLine("Open connect. Press any button");
    Console.ReadKey();
    // code for option 2
    FileReceiver_REST_API.Main();

}
else
{
    Console.WriteLine("Incorrect option");
}

Console.ReadKey();


//while (Console.ReadKey().Key != ConsoleKey.Enter)
//{
//    Filer.Start();
//}







