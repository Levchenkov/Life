using System.Collections;
using System.Collections.Generic;

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
    }
}