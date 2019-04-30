using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNet.SignalR.Messaging;
using Newtonsoft.Json;

namespace Life.Server.Host.ConsoleApp
{
    public class GameHostHolder
    {
        private static GameHost host; 
        public static GameHost Host
        {
            get
            {
                if (host == null)
                {
                    Init();
                }

                return host;
            }
        }

        public static IDictionary<string, GameClient> Clients { get; set; } = new Dictionary<string, GameClient>();

        public static void Init()
        {
            lock (new object())
            {
                if (host == null)
                {
                    host = new GameHost();
                }
            }
        }

        public static void Connect(string clientId)
        {
            if (Clients.ContainsKey(clientId))
            {
                Clients[clientId].IsConnected = true;
            }
            else
            {
                Clients.Add(clientId, new GameClient { ClientId = clientId, IsConnected = true });
            }
        }

        public static void Disconnect(string clientId)
        {
            if (Clients.ContainsKey(clientId))
            {
                Clients[clientId].IsConnected = false;
            }
        }

        public static bool IsClientConnected(string clientId)
        {
            return Clients.ContainsKey(clientId) && Clients[clientId].IsConnected;
        }

        public static Queue<ICommand> CommandQueue = new Queue<ICommand>();
    }

    public interface ICommand
    {
        void Execute();
    }

    public struct GetFieldCommand : ICommand
    {
        private readonly IClient client;

        public GetFieldCommand(IClient client)
        {
            this.client = client;
        }

        public void Execute()
        {
            var serializedField = JsonConvert.SerializeObject(GameHostHolder.Host.Game.Field);
            client.SendField(serializedField);
        }
    }
}