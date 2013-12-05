using System;
using LabirynthConsole.Static;

namespace LabirynthConsole
{
    public class Player
    {
        private string name;
        private Coordinate currentPosition;
        private Coordinate finishPosition;
        private char[][] map;
        private bool[][] visible;
        public Player(string name, Coordinate startPosition, char[][] charMap)
        {
            this.name = name;
            currentPosition = new Coordinate(startPosition.j * 2 + 1, startPosition.i * 2 + 1);
            map = charMap;
            visible = new bool[map.Length][];
            for (int i = 0; i < map.Length; i++) visible[i] = new bool[map[0].Length];
            visible[currentPosition.i][currentPosition.j] = true;
        }

        public void setPosition(int x, int y)
        {
            currentPosition.i = y;
            currentPosition.j = x;
        }

        public void addVisible(int x, int y)
        {
            visible[y][x] = true;
        }

        public void DrawMap(int left, int top)
        {
            Console.SetCursorPosition(left, top);
            for(int i = 0; i < map.Length; i++)
            {
                Console.CursorLeft = left;
                for(int j = 0; j < map[i].Length; j++)
                {
                    if(visible[i][j])
                    {
                        if(currentPosition.j == j && currentPosition.i == i)
                            Console.Write("*");
                        else
                            Console.Write(map[i][j]);
                    }
                    else Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

        public Coordinate getPosition()
        {
            return currentPosition;
        }

        public void changePosition(int direction)
        {
            if(direction == Direction.Up)
            {
                setPosition(currentPosition.j, currentPosition.i - 2);
            }
            else if(direction == Direction.Down)
            {
                setPosition(currentPosition.j, currentPosition.i + 2);
            }
            else if(direction == Direction.Left)
            {
                setPosition(currentPosition.j - 2, currentPosition.i);
            }
            else if(direction == Direction.Right)
            {
                setPosition(currentPosition.j + 2, currentPosition.i);
            }
        }

        public string getName()
        {
            return name;
        }
    }
}
