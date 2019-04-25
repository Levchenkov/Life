namespace Life.Server.Core
{
    public class GameManager
    {
        private readonly FieldManager fieldManager;

        public GameManager(FieldManager fieldManager)
        {
            this.fieldManager = fieldManager;
        }

        public void Update(Game game)
        {
            fieldManager.Update(game.Field);
        }
    }
}