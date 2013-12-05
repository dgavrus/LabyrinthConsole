using System;
using System.Collections;
using System.IO;
using System.Threading;
using LabirynthConsole.Static;

namespace LabirynthConsole
{
    public class Game
    {
        public bool play { get; private set; } 
        private Player[] players;
        private Player activePlayer;
        private Map map;
        private string path = Environment.CurrentDirectory.ToString() + "\\";
        private static int leftDrawPos;
        private static int topDrawPos;

        public Game()
        {
            map = new Map(getMap());
            if (map.getSquares() != null)
            {
                play = true;
                players = getPlayers(getPlayersNames());

                // Set labirynth draw positions with random offset from left and top 
                leftDrawPos = (map.charWidth * 2 >= Console.WindowWidth) ? map.charWidth / 2 + new Random().Next(map.charWidth / 2)
                                  : new Random().Next(map.charWidth);
                topDrawPos = (map.charHeigth * 2 >= Console.WindowHeight - 2) ? map.charHeigth / 2 + new Random().Next(map.charHeigth / 2)
                                 : new Random().Next(map.charHeigth) + 2;
            }
            else play = false;
        }

        public void start()
        {
            Player winner = null;
            int activePlayerIndex = new Random().Next(players.Length);
            activePlayer = players[activePlayerIndex];
            while(play)
            {
                Console.WriteLine("Ход игрока " + activePlayer.getName());
                Console.WriteLine("Текущее положение");
                activePlayer.DrawMap(leftDrawPos,topDrawPos);
                Console.WriteLine("Ожидание хода игрока " + activePlayer.getName() + "...");
                switch (Console.ReadKey(false).Key)
                {
                    case ConsoleKey.UpArrow:
                        eraseText(0,2 + topDrawPos + map.charHeigth,20);
                        if((map.getSquares()[activePlayer.getPosition().i/2,activePlayer.getPosition().j/2].walls & Wall.Top) == Wall.Top)
                        {
                            Console.WriteLine("Сверху стенка");
                            activePlayer.addVisible(activePlayer.getPosition().j,activePlayer.getPosition().i - 1);
                        }
                        else
                        {
                            Console.WriteLine("Проходишь");
                            activePlayer.addVisible(activePlayer.getPosition().j,activePlayer.getPosition().i - 1);
                            activePlayer.addVisible(activePlayer.getPosition().j, activePlayer.getPosition().i - 2);
                            activePlayer.addVisible(activePlayer.getPosition().j + 1, activePlayer.getPosition().i - 1);
                            activePlayer.addVisible(activePlayer.getPosition().j - 1, activePlayer.getPosition().i - 1);
                            activePlayer.changePosition(Direction.Up);
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        eraseText(0, 2 + topDrawPos + map.charHeigth, 20);
                        if((map.getSquares()[activePlayer.getPosition().i/2,activePlayer.getPosition().j/2].walls & Wall.Bottom) == Wall.Bottom)
                        {
                            Console.WriteLine("Снизу стенка");
                            activePlayer.addVisible(activePlayer.getPosition().j,activePlayer.getPosition().i + 1);
                        }
                        else
                        {
                            Console.WriteLine("Проходишь");
                            activePlayer.addVisible(activePlayer.getPosition().j,activePlayer.getPosition().i + 1);
                            activePlayer.addVisible(activePlayer.getPosition().j, activePlayer.getPosition().i + 2);
                            activePlayer.addVisible(activePlayer.getPosition().j + 1, activePlayer.getPosition().i + 1);
                            activePlayer.addVisible(activePlayer.getPosition().j - 1, activePlayer.getPosition().i + 1);
                            activePlayer.changePosition(Direction.Down);
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        eraseText(0, 2 + topDrawPos + map.charHeigth, 20);
                        if((map.getSquares()[activePlayer.getPosition().i/2,activePlayer.getPosition().j/2].walls & Wall.Right) == Wall.Right)
                        {
                            Console.WriteLine("Справа стенка");
                            activePlayer.addVisible(activePlayer.getPosition().j + 1,activePlayer.getPosition().i);
                        }
                        else
                        {
                            Console.WriteLine("Проходишь");
                            activePlayer.addVisible(activePlayer.getPosition().j + 1,activePlayer.getPosition().i);
                            activePlayer.addVisible(activePlayer.getPosition().j + 2, activePlayer.getPosition().i);
                            activePlayer.addVisible(activePlayer.getPosition().j + 1, activePlayer.getPosition().i - 1);
                            activePlayer.addVisible(activePlayer.getPosition().j + 1, activePlayer.getPosition().i + 1);
                            activePlayer.changePosition(Direction.Right);
                            
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        eraseText(0, 2 + topDrawPos + map.charHeigth, 20);
                        if ((map.getSquares()[activePlayer.getPosition().i / 2, activePlayer.getPosition().j / 2].walls & Wall.Left) == Wall.Left)
                        {
                            Console.WriteLine("Слева стенка");
                            activePlayer.addVisible(activePlayer.getPosition().j - 1, activePlayer.getPosition().i);
                        }
                        else
                        {
                            Console.WriteLine("Проходишь");
                            activePlayer.addVisible(activePlayer.getPosition().j - 1, activePlayer.getPosition().i);
                            activePlayer.addVisible(activePlayer.getPosition().j - 2, activePlayer.getPosition().i);
                            activePlayer.addVisible(activePlayer.getPosition().j - 1, activePlayer.getPosition().i - 1);
                            activePlayer.addVisible(activePlayer.getPosition().j - 1, activePlayer.getPosition().i + 1);
                            activePlayer.changePosition(Direction.Left);
                        }
                        break;   
                }
                if (activePlayer.getPosition().i == map.getFinish().i && 
                        activePlayer.getPosition().j == map.getFinish().j)
                {
                    winner = activePlayer;
                    play = false;
                }
                Thread.Sleep(500);
                Console.Clear();
                Console.WriteLine("Новое положение");
                activePlayer.DrawMap(leftDrawPos, topDrawPos);
                Thread.Sleep(1100);
                Console.Clear();
                players[activePlayerIndex] = activePlayer;
                activePlayer = players[activePlayerIndex];
                activePlayer = getActivePlayer(activePlayerIndex);
                activePlayerIndex = (activePlayerIndex + 1)%players.Length;
            }
            Console.WriteLine("Победитель " + winner.getName());
            Console.WriteLine("Нажмите Enter для продолжения...");
            while (Console.ReadKey(false).Key != ConsoleKey.Enter) ;  
        }

        private void eraseText(int left, int top, int textLength)
        {
            Console.SetCursorPosition(left, top);
            for (int i = 0; i < textLength; i++) Console.Write(" ");
            Console.SetCursorPosition(left, top);
        }

        private Player getActivePlayer(int player)
        {
            return players[(player + 1) % players.Length];
        }

        private void step()
        {
            
        }

        private char[][] getMap()
        {

            string mapFile = "maps.mp";
            char[][] map = new char[0][];
            Console.WriteLine("Выберите карту:");
            FileParser fileParser = new FileParser(mapFile); // Parsing maps file to know maps dimensions
            ArrayList mapInfo = fileParser.getMapInfo();
            int topCursorPos = 1;
            int leftCursorPos = 2;
            try
            {
                foreach (string[] info in mapInfo)
                {
                    Console.CursorLeft = leftCursorPos;
                    Console.WriteLine(info[0] + ' ' + info[1]);
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Wrong map file configuration!");
                return null;
            }
            topCursorPos = 1;
            leftCursorPos = 1;
            Console.CursorVisible = true;
            Console.SetCursorPosition(leftCursorPos, topCursorPos);
            Console.CursorSize = 1;
            bool selectedMap = false;
            while(!selectedMap)
            {
                ConsoleKeyInfo key = Console.ReadKey(false);
                switch(key.Key)
                {
                    case ConsoleKey.UpArrow:
                        topCursorPos = topCursorPos > 1 ? topCursorPos - 1 : topCursorPos;
                        Console.SetCursorPosition(leftCursorPos, topCursorPos);
                        break;
                    case ConsoleKey.DownArrow:
                        topCursorPos = topCursorPos < mapInfo.Count ? topCursorPos + 1 : topCursorPos;
                        Console.SetCursorPosition(leftCursorPos, topCursorPos);
                        break;
                    case ConsoleKey.Enter:
                        map = getMapFromFile((string[])mapInfo[Console.CursorTop - 1], mapFile);
                        int cursorTop = Console.CursorTop;
                        Console.Clear();
                        Console.WriteLine("Выбрана карта " + ((string[])mapInfo[cursorTop - 1])[1]);
                        selectedMap = true;
                        Console.CursorVisible = false;
                        break;
                    default:
                        topCursorPos = 1;
                        Console.SetCursorPosition(leftCursorPos, topCursorPos);
                        break;
                }
            }
            return map;
        } 

        private char[][] getMapFromFile(string[] mapInfo, string mapFile)
        {
            StreamReader fin = new StreamReader(new FileStream(path + mapFile, FileMode.Open));
            string[] parseDim = mapInfo[1].Split('x','.');
            int heigth = Int32.Parse(parseDim[0]) * 2 + 1;
            int width = Int32.Parse(parseDim[1]) * 2 + 1;
            char[][] map = new char[heigth][];
            while (!fin.ReadLine().Split(' ')[0].Equals(mapInfo[0]));
            for (int i = 0; i < heigth; i++)
            {
                //if (fin.EndOfStream) break;
                string tmp = fin.ReadLine();
                map[i] = new char[tmp.Length];
                for (int k = 0; k < tmp.Length; k++)
                {
                    map[i][k] = tmp[k];
                }
            }
            fin.Close();
            return map;
        }

        private string[] getPlayersNames()
        {
            int? n = null;
            while(n > 4 || n == null)
            {
                Console.WriteLine("Сколько игроков будет играть?");
                try
                {
                    n = Int32.Parse(Console.ReadLine());
                }
                catch (System.FormatException e)
                {
                    Console.WriteLine(e.Message);
                    n = null;
                    Thread.Sleep(1000);
                }
                System.Console.Clear();
            }
            string[] result = new string[(int)n];
            for(int i = 0; i < n; i++)
            {
                Console.WriteLine("Введите имя игрока " + (int)(i + 1)  + ":");
                result[i] = Console.ReadLine();
            }
            Console.Clear();
            return result;
        }

        private Player[] getPlayers(string[] names)
        {
            Player[] result = new Player[names.Length];
            for(int i = 0; i < names.Length; i++)
            {
                result[i] = new Player(names[i], map.getStart(), map.getCharMap());
            }
            return result;
        }
    }
}
