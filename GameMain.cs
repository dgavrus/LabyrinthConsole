using System;

namespace LabirynthConsole
{
    class GameMain
    {
        static void Main(string[] args)
        {

            bool play = true; 
            while(play) 
            {
                Game game = new Game();
                if(game.play)
                    game.start();
                Console.Clear();
                Console.WriteLine("Хотите сыграть ещё?");
                Console.WriteLine(" Да");
                Console.WriteLine(" Нет");
                Console.SetCursorPosition(0,1);
                Console.CursorVisible = true;
                bool selected = false;
                while(!selected)
                {
                    switch(Console.ReadKey().Key)
                    {
                        case ConsoleKey.DownArrow:
                            Console.SetCursorPosition(0,2);
                            break;
                        case ConsoleKey.UpArrow:
                            Console.SetCursorPosition(0, 1);
                            break;
                        case ConsoleKey.Enter:
                            if (Console.CursorTop == 2) play = false;
                            else Console.Clear();
                            selected = true;
                            break;
                        default:
                            Console.SetCursorPosition(0, 1);
                            break;
                    }
                }
            }
            
        }
    }
}
