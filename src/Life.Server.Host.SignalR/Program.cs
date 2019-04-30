using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace Life.Server.Host.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            InitGameCore();
            // This will *ONLY* bind to localhost, if you want to bind to all addresses
            // use http://*:8080 to bind to all addresses. 
            // See http://msdn.microsoft.com/library/system.net.httplistener.aspx 
            // for more information.
            string url = "http://localhost:8088";
            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("Server running on {0}", url);
                Console.ReadLine();
            }
        }

        private static void InitGameCore()
        {
            GameHostHolder.Init();

            Task.Run(async () =>
            {
                while (true)
                {
                    GameHostHolder.Host.GameManager.Update(GameHostHolder.Host.Game);
                    await Task.Delay(50);
                }
            });

            Task.Run(async () =>
            {
                while (true)
                {
                    if (GameHostHolder.CommandQueue.Count > 0)
                    {
                        var command = GameHostHolder.CommandQueue.Dequeue();
                        command.Execute();
                    }
                    else
                    {
                        await Task.Delay(50);
                    }
                }
            });
        }
    }
}
