using Life.Server.Core;

namespace Life.Server.Host.ConsoleApp
{
    public class GameHost
    {
        public GameManager GameManager { get; set; }
        public Game Game { get; set; }

        public GameHost()
        {
            var fieldManager = new FieldManager();
            var gameBuilder = new GameBuilder(fieldManager);
            Game = gameBuilder.Build();

            GameManager = new GameManager(fieldManager);
        }
    }
}