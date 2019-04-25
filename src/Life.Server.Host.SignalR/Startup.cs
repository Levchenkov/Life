using Microsoft.Owin;
using Owin;

namespace Life.Server.Host.ConsoleApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.AddSignalRNetwork();
        }
    }
}
