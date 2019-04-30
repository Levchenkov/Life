using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Life.Client.Network.SignalR;

namespace Life.Client.Launcher
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var count = 100;

            for (int j = 0; j < count; j++)
            {
                StartClientForms();
                //await StartNativeClient();
                Console.WriteLine($"Started {j}");
                Thread.Sleep(500);
            }

            Console.ReadLine();
        }

        static void StartClientForms()
        {
            Process.Start(@"C:\Dev\eugene\Life\git\src\Life.Client.Forms\bin\Debug\Life.Client.Forms.exe");
        }

        static async Task StartNativeClient()
        {
            var serverConnection = new ServerConnection("http://localhost:8088");
            await serverConnection.OpenAsync();
            serverConnection.OnReceive<string>("SendField", message =>
            {
                //var field = JsonConvert.DeserializeObject<Field>(message);
                //game.Field = field;
                //ShowCmd();
            });

            await serverConnection.Send("Connect");

            await Task.Run(async () =>
            {
                while (true)
                {
                    await serverConnection.Send("GetField");
                    await Task.Delay(50);
                }
            });
        }
    }
}
