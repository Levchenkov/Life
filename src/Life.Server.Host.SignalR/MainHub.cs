using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;

namespace Life.Server.Host.ConsoleApp
{
    public class MainHub : Hub<IClient>
    {
        public void Connect()
        {
            Console.WriteLine("Connected");
            GameHostHolder.Connect(Context.ConnectionId);
            //Task.Run(async () =>
            //{
            //    while (GameHostHolder.IsClientConnected(Context.ConnectionId))
            //    {
            //        var serializedField = JsonConvert.SerializeObject(GameHostHolder.Host.Game.Field);
            //        Clients.Caller.SendField(serializedField);
            //        //Console.WriteLine($"{DateTime.Now} SendField {serializedField.Length * 2} bytes to {Context.ConnectionId}");
            //        await Task.Delay(20);
            //    }
            //});
        }

        public void GetField()
        {
            GameHostHolder.CommandQueue.Enqueue(new GetFieldCommand(Clients.Caller));
        }

        public void Disconnect()
        {
            Console.WriteLine("Soft Disconnected");
            GameHostHolder.Disconnect(Context.ConnectionId);
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            Console.WriteLine("Hard Disconnected");
            GameHostHolder.Disconnect(Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }
    }
}
