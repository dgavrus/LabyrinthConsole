using System;
using System.Configuration;
using System.Threading;

namespace LabirynthConsole
{
    public class Map
    {      
        private Square[ , ] squares;
        private char[][] charMap;
        private Coordinate startSquare;
        private Coordinate finishSquare;
        public int mapWidth { get { return charMap[0].Length/2; } }
        public int mapHeigth { get { return charMap.Length/2; } }
        public int charHeigth { get { return charMap.Length; } }
        public int charWidth { get { return charMap[0].Length; } }

        public Map(int width, int heigth)
        {
            //TODO
            squares = Generator.GenerateMap(heigth, width);
        }

        public Map(char[][] map)
        {
            squares = getSquares(map);
            try
            {
                Coordinate[] startfinish = getEnterExitSquare(map);
                if (startfinish == null) throw new Exception("Map error");
                 startSquare = startfinish[0];
                 finishSquare = startfinish[1];
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Thread.Sleep(500);
                squares = null;
            }
            charMap = map;
        }

        public Coordinate getStart()
        {
            return startSquare;
        }

        public Coordinate getFinish()
        {
            return new Coordinate(finishSquare.j * 2 + 1, finishSquare.i * 2 + 1);
        }

        public char[][] getCharMap()
        {
            return charMap;
        }

        public Square[,] getSquares()
        {
            return squares;
        }

        private Square[,] getSquares(char[][] map)
        {
            int heigth = map.Length / 2;
            int width = map[0].Length / 2;
            Square[,] result = new Square[heigth,width];
            for (int i = 0; i < heigth; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    int walls = 0;
                    if (map[i * 2 + 1][j * 2] == Wall.CharWall)
                        walls += Wall.Left;
                    if (map[i * 2][j * 2 + 1] == Wall.CharWall)
                        walls += Wall.Top;
                    if (map[i * 2 + 1][j * 2 + 2] == Wall.CharWall)
                        walls += Wall.Right;
                    if (map[i * 2 + 2][j * 2 + 1] == Wall.CharWall)
                        walls += Wall.Bottom;
                    result[i,j] = new Square(walls);
                }
            }
            return result;
        }

        private Coordinate[] getEnterExitSquare(char[][] map)
        {
            int heigth = map.Length / 2;
            int width = map[0].Length / 2;
            Coordinate[] result = new Coordinate[2];
            byte start = 0;
            byte finish = 0;
            for(int i = 0; i < heigth; i++)
            {
                for(int j = 0; j < width; j++)
                {
                    if(map[i * 2 + 1][j * 2 + 1] == 'S')
                    {
                        result[0] = new Coordinate(j, i);
                        start++;
                    }
                    if (map[i * 2 + 1][j * 2 + 1] == 'X')
                    {
                        result[1] = new Coordinate(j, i);
                        finish++;
                    }
                    if (start == 1 && finish == 1) return result;
                }
            }
            return null;
        }
    }
}
