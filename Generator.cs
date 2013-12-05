using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabirynthConsole
{
    public static class Generator
    {
        public static Square[,] GenerateMap(int width, int heigth)
        {
            bool finished = false;
            bool started = false;
            Square[,] result = new Square[heigth, width];
            for (int i = 0; i < heigth; i++)
            {
                for(int j = 0; j < width; j++)
                {
                    result[i, j] = new Square();
                }
            }
            return result;
        }
    }
}
