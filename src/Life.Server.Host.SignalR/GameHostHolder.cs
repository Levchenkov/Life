using System.Collections;
using System.Collections.Concurrent;
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

                if (CommandQueue == null)
                {
                    CommandQueue = new ConcurrentQueue<ICommand>();
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

        public static ConcurrentQueue<ICommand> CommandQueue;
    }

    public interface ICommand
    {
        void Execute();
    }

    public struct GetFieldCommand : ICommand
    {
        private readonly IClient client;
        private readonly string connectionId;

        public GetFieldCommand(string connectionId, IClient client)
        {
            this.connectionId = connectionId;
            this.client = client;
        }

        public void Execute()
        {
            if (GameHostHolder.IsClientConnected(connectionId))
            {
                var serializedField = JsonConvert.SerializeObject(GameHostHolder.Host.Game.Field);
                client.SendField(serializedField);
            }
        }
    }
}