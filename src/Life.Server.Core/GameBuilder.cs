namespace Life.Server.Core
{
    public class GameBuilder
    {
        private readonly FieldManager fieldManager;
        private Field field;

        public GameBuilder(FieldManager fieldManager)
        {
            this.fieldManager = fieldManager;
        }

        public Game Build()
        {
            if (field == null)
            {
                field = fieldManager.CreateEmpty();
            }
            return new Game
            {
                Field = field
            };
        }
    }
}