using System.Collections.Generic;
using System.Linq;

namespace Life.Server.Core
{
    public class FieldManager
    {
        public Field CreateEmpty()
        {
            return new Field(20, 30);
        }

        public void Update(Field field)
        {
            var map = new bool[field.Height, field.Width];
            for (int y = 0; y < field.Height; y++)
            {
                for (int x = 0; x < field.Width; x++)
                {
                    var aliveCount = GetNeighboursTor(field, x , y).Count(value => value);

                    if (field.Map[y, x])
                    {
                        map[y, x] = aliveCount > 1 && aliveCount < 4;
                    }
                    else
                    {
                        map[y, x] = aliveCount == 3;
                    }
                }
            }

            field.Map = map;
        }

        private IEnumerable<bool> GetNeighbours(Field field, int x, int y)
        {
            var xIndexes = new[] {x - 1, x, x + 1}.Where(value => value >= 0 && value <= field.Width - 1).ToList();
            var yIndexes = new[] {y - 1, y, y + 1}.Where(value => value >= 0 && value <= field.Height - 1).ToList();

            var result = new List<bool>();
            foreach (int xValue in xIndexes)
            foreach (var yValue in yIndexes)
            {
                if (!(xValue == x && yValue == y))
                {
                    result.Add(field.Map[yValue, xValue]);
                }
            }

            return result;
        }

        private IEnumerable<bool> GetNeighboursTor(Field field, int x, int y)
        {
            yield return GetNeighbour1(field, x, y);
            yield return GetNeighbour2(field, x, y);
            yield return GetNeighbour3(field, x, y);
            yield return GetNeighbour4(field, x, y);
            yield return GetNeighbour6(field, x, y);
            yield return GetNeighbour7(field, x, y);
            yield return GetNeighbour8(field, x, y);
            yield return GetNeighbour9(field, x, y);
        }

        private bool GetNeighbour1(Field field, int x, int y)
        {
            int nx = x - 1;
            int ny = y - 1;
            if (x == 0)
            {
                nx = field.Width - 1;
            }

            if (y == 0)
            {
                ny = field.Height - 1;
            }

            return field.Map[ny, nx];
        }

        private bool GetNeighbour2(Field field, int x, int y)
        {
            int nx = x;
            int ny = y - 1;

            if (y == 0)
            {
                ny = field.Height - 1;
            }

            return field.Map[ny, nx];
        }

        private bool GetNeighbour3(Field field, int x, int y)
        {
            int nx = x + 1;
            int ny = y - 1;

            if (x == field.Width - 1)
            {
                nx = 0;
            }

            if (y == 0)
            {
                ny = field.Height - 1;
            }

            return field.Map[ny, nx];
        }

        private bool GetNeighbour4(Field field, int x, int y)
        {
            int nx = x - 1;
            int ny = y;
            if (x == 0)
            {
                nx = field.Width - 1;
            }

            return field.Map[ny, nx];
        }

        private bool GetNeighbour6(Field field, int x, int y)
        {
            int nx = x + 1;
            int ny = y;
            if (x == field.Width - 1)
            {
                nx = 0;
            }

            return field.Map[ny, nx];
        }

        private bool GetNeighbour7(Field field, int x, int y)
        {
            int nx = x - 1;
            int ny = y + 1;
            if (x == 0)
            {
                nx = field.Width - 1;
            }

            if (y == field.Height - 1)
            {
                ny = 0;
            }

            return field.Map[ny, nx];
        }

        private bool GetNeighbour8(Field field, int x, int y)
        {
            int nx = x;
            int ny = y + 1;
            
            if (y == field.Height - 1)
            {
                ny = 0;
            }

            return field.Map[ny, nx];
        }

        private bool GetNeighbour9(Field field, int x, int y)
        {
            int nx = x + 1;
            int ny = y + 1;
            if (x == field.Width - 1)
            {
                nx = 0;
            }

            if (y == field.Height - 1)
            {
                ny = 0;
            }

            return field.Map[ny, nx];
        }
    }
}