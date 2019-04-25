using Microsoft.AspNet.SignalR;

namespace Life.Server.Host.ConsoleApp
{
    public class MainHub : Hub<IClient>
    {
        public void GetField()
        {
            Clients.Caller.SendField("Hello");
        }
    }
}
