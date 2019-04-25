using System;
using System.Text;

namespace Life.Server.Core
{
    public class Field
    {
        public int Height { get; }
        public int Width { get; }

        public bool[,] Map { get; set; }

        public Field(int height, int width)
        {
            Height = height;
            Width = width;
            Map = new bool[Height, Width];
            Map[0, 1] = true;
            Map[1, 2] = true;
            Map[2, 0] = true;
            Map[2, 1] = true;
            Map[2, 2] = true;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    result.Append(Convert.ToByte(Map[y, x]));
                }

                result.Append('\n');
            }

            return result.ToString();
        }
    }
}
