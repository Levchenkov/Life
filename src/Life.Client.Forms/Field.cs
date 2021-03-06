﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Life.Client.Forms
{
    public class Field
    {
        public int Height { get; set; }
        public int Width { get; set; }

        public bool[,] Map { get; set; }

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

    public class Game
    {
        public Field Field { get; set; }
    }
}
