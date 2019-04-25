using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Owin;

namespace Life.Server.Host.ConsoleApp
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/aspnet/signalr/overview/deployment/tutorial-signalr-self-host
    /// </summary>
    public static class AppBuilderExtensions
    {
        public static IAppBuilder AddSignalRNetwork(this IAppBuilder appBuilder)
        {
            //GlobalHost.DependencyResolver.Register(
            //    typeof(MainHub),
            //    () => new MainHub(new ChatMessageRepository()));

            appBuilder.UseCors(CorsOptions.AllowAll);
            appBuilder.MapSignalR();
            return appBuilder;
        }
    }
}
