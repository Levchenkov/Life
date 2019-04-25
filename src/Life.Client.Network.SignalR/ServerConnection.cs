using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;

namespace Life.Client.Network.SignalR
{
    public class ServerConnection
    {
        private readonly HubConnection hubConnection;
        private readonly IHubProxy hubProxy;

        public ServerConnection(string url)
        {
            hubConnection = new HubConnection(url);
            hubProxy = hubConnection.CreateHubProxy("MainHub");
        }

        public Task OpenAsync()
        {
            return hubConnection.Start();
        }

        public Task Send(string method, params object[] parameters)
        {
            return hubProxy.Invoke(method, parameters);
        }

        public Task<TReturn> Send<TReturn>(string method, params object[] parameters)
        {
            return hubProxy.Invoke<TReturn>(method, parameters);
        }

        public void OnReceive(string eventName, Action action)
        {
            hubProxy.On(eventName, action);
        }

        public void OnReceive<TParam>(string eventName, Action<TParam> action)
        {
            hubProxy.On(eventName, action);
        }

        public void Close()
        {
            hubConnection.Stop();
        }
    }
}
