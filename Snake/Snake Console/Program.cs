using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snake_Logic;
namespace Snake_Console
{
    class Program
    {
        static Plataform plataform;
        static void Main(string[] args)
        {
           plataform = new Plataform(10,10,1500);
            plataform.MoveSnakeEvent += new Plataform.MoveSnakeEventHandler((object sender,Snake_Logic.Event_Args.MoveSnakeArgs moveArgs)=> {
                Console.Clear();
                for (int x = (-1); x < plataform.Width+1; x++)
                {
                    for (int y = (-1); y < plataform.Height+1; y++)
                    {
                        var px = plataform.GetContentInPoint(new Snake_Logic.Base.Point(x,y));
                        switch (px)
                        {
                            case Snake_Logic.Enums.PointCotent.Null:
                                Console.Write(".");
                                break;
                            case Snake_Logic.Enums.PointCotent.Wall:
                                Console.Write("W");
                                break;
                            case Snake_Logic.Enums.PointCotent.Apple:
                                Console.Write("A");
                                break;
                            case Snake_Logic.Enums.PointCotent.SnakeBody:
                                Console.Write("S");
                                break;
                        }
                    }
                    Console.WriteLine();
                }
            });
            plataform.LoseGame += new Plataform.LoseGameHandler(Lose);
            plataform.PlayGame();
            ConsoleKeyInfo consoleKey;
            do
            {
                consoleKey = Console.ReadKey();
                try
                {
                    if (consoleKey.Key == ConsoleKey.UpArrow)
                    {
                        plataform.Snake.MoveUP();
                    }
                    if (consoleKey.Key == ConsoleKey.DownArrow)
                    {
                        plataform.Snake.MoveDown();
                    }
                    if (consoleKey.Key == ConsoleKey.LeftArrow)
                    {
                        plataform.Snake.MoveLeft();
                    }
                    if (consoleKey.Key == ConsoleKey.RightArrow)
                    {
                        plataform.Snake.MoveRight();
                    }
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("Ei!");
                }
            } while (consoleKey.Key!=ConsoleKey.Escape);
        }
        private static void Lose(object sender, Snake_Logic.Event_Args.LoseGameArgs LoseArg) {
            plataform.PauseGame();
            plataform = new Plataform(10, 10, 1500);
            plataform.MoveSnakeEvent += new Plataform.MoveSnakeEventHandler((object sendeer, Snake_Logic.Event_Args.MoveSnakeArgs moveArgs) => {
                Console.Clear();
                for (int x = (-1); x < plataform.Height+1; x++)
                {
                    for (int y = (-1); y < plataform.Width+1; y++)
                    {
                        var px = plataform.GetContentInPoint(new Snake_Logic.Base.Point(x, y));
                        switch (px)
                        {
                            case Snake_Logic.Enums.PointCotent.Null:
                                Console.Write(".");
                                break;
                            case Snake_Logic.Enums.PointCotent.Wall:
                                Console.Write("W");
                                break;
                            case Snake_Logic.Enums.PointCotent.Apple:
                                Console.Write("A");
                                break;
                            case Snake_Logic.Enums.PointCotent.SnakeBody:
                                Console.Write("S");
                                break;
                        }
                    }
                    Console.WriteLine();
                }
            });
            plataform.LoseGame += new Plataform.LoseGameHandler(Lose);
            Console.WriteLine("Lose Game!");
            plataform.PlayGame();
        }  
    }
}
