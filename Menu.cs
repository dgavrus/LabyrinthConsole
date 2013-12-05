using System;
using System.Collections;
using System.IO;

namespace LabirynthConsole
{
    public class Menu
    {
        private string path = Environment.CurrentDirectory.ToString() + "\\";

        public char[][] mainMenu()
        {
            char[][] map = new char[0][];
            ArrayList maps = getMaps();
            Console.WriteLine(" Главное меню");
            foreach(string curr in maps)
            {
                Console.WriteLine("  " + curr);
            }
            int topCursorPos = 1;
            int leftCursorPos = 0;
            Console.Write('>');
            bool selected = false;
            while(!selected)
            {
                ConsoleKeyInfo key = Console.ReadKey(false);
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if(topCursorPos > 1)
                        {
                            Console.Write(' ');
                            leftCursorPos--;
                            topCursorPos--;
                            Console.Write('>');
                            leftCursorPos--;
                        }
                        Console.SetCursorPosition(leftCursorPos, topCursorPos);
                        break;
                    case ConsoleKey.DownArrow:
                        if(topCursorPos < maps.Count)
                        {
                            Console.Write(' ');
                            leftCursorPos--;
                            topCursorPos++;
                            Console.Write('>');
                            leftCursorPos--;
                        }
                        Console.SetCursorPosition(leftCursorPos, topCursorPos);
                        break;
                    case ConsoleKey.Enter:
                        if(Console.CursorTop >= 1 && Console.CursorTop <= maps.Count)
                        {
                            
                        }
                        for
                        switch (Console.CursorTop)
                        {
                            case 1:
                                map = getMapFromFile("3x5", "maps.mp");
                                Console.Clear();
                                Console.WriteLine("Выбрана карта 3x5");
                                selectedMap = true;
                                break;
                            case 2:
                                map = getMapFromFile("5x8", "maps.mp");
                                Console.Clear();
                                Console.WriteLine("Выбрана карта 5x8");
                                selectedMap = true;
                                break;
                            case 3:
                                map = getMapFromFile("7x11", "maps.mp");
                                Console.Clear();
                                Console.WriteLine("Выбрана карта 7x11");
                                selectedMap = true;
                                break;
                        }
                        Console.CursorVisible = false;
                        break;
                    default:
                        topCursorPos = 1;
                        Console.SetCursorPosition(leftCursorPos, topCursorPos);
                        break;    
                }
            }
            /*
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
                        topCursorPos = topCursorPos < 2 ? topCursorPos + 1 : topCursorPos;
                        Console.SetCursorPosition(leftCursorPos, topCursorPos);
                        break;
                    case ConsoleKey.Enter:  
                        switch (Console.CursorTop)
                        {
                            case 1:
                                map = getMapFromFile("3x5","maps.mp");
                                Console.Clear();
                                Console.WriteLine("Выбрана карта 3x5");
                                selectedMap = true;
                                break;
                            case 2:
                                map = getMapFromFile("5x8","maps.mp");
                                Console.Clear();
                                Console.WriteLine("Выбрана карта 5x8");
                                selectedMap = true;
                                break;
                            case 3:
                                map = getMapFromFile("7x11","maps.mp");
                                Console.Clear();
                                Console.WriteLine("Выбрана карта 7x11");
                                selectedMap = true;
                                break;
                        }
                        Console.CursorVisible = false;
                        break;
                    default:
                        topCursorPos = 1;
                        Console.SetCursorPosition(leftCursorPos, topCursorPos);
                        break;
                }
            }
            /**/
            return map;     
        }

        private ArrayList getMaps()
        {
            StreamReader fin = new StreamReader(new FileStream(path + "maps.mp", FileMode.Open));
            ArrayList result = new ArrayList();
            while(true)
            {
                string curr = fin.ReadLine();
                if (curr.Equals(fin.EndOfStream))
                    break;
                if(curr.Contains("x"))
                {
                    result.Add(curr);
                }
            }
            fin.Close();
            return result;
        }
    }
}
