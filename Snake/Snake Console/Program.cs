using System;
using System.Threading;
using Snake.Logic;
using Snake.Logic.Base;
using Snake.Logic.Enums;
using Snake.Logic.EventArgs;
using Snake.Logic.Graphic;
using Snake.Logic.Graphic.Base;
using Snake.Logic.Graphic.EventArgs;

namespace Snake.Console
{
    using Console = System.Console;
    class Program
    {
        static GraphicGamePlataform plataform => gameUI.GamePlataform;
        static GameUI gameUI;
        static int Width = 10;
        static int Height = 10;
        static int Velocity = 750;
        static Random rd;
        static void Main(string[] args)
        {
            
            
            ConsoleKeyInfo consoleKey;
            do
            {
                consoleKey = Console.ReadKey();
                try
                {
                    if (consoleKey.Key == ConsoleKey.UpArrow)
                    {
                        gameUI.GamePlataform.Snake.MoveUP();
                    }
                    if (consoleKey.Key == ConsoleKey.DownArrow)
                    {
                        gameUI.GamePlataform.Snake.MoveDown();
                    }
                    if (consoleKey.Key == ConsoleKey.LeftArrow)
                    {
                        gameUI.GamePlataform.Snake.MoveLeft();
                    }
                    if (consoleKey.Key == ConsoleKey.RightArrow)
                    {
                        gameUI.GamePlataform.Snake.MoveRight();
                    }
                    if (consoleKey.Key == ConsoleKey.K)
                    {
                        gameUI.GamePlataform.Snake.Kill(KillCause.User);
                    }
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("Ei!");
                }
            } while (consoleKey.Key != ConsoleKey.Escape);
        }
        private static void Lose(object sender, LoseGameArgs LoseArg)
        {
            gameUI.GamePlataform = new GraphicGamePlataform(Width, Height, Velocity);
            gameUI.GamePlataform.UpdateView += new GamePlataform.UpdateViewHandler(UpdateView);
            gameUI.GamePlataform.LoseGame += new GamePlataform.LoseGameHandler(Lose);
            for (int i = 0; i < rd.Next(0, Width / 2); i++)
            {
                gameUI.GamePlataform.Objects.Add(new DefaultObject(plataform.Size, new Point(rd.Next(2, Width), rd.Next(2, Width)), ObjectContent.Solid, ObjectType.Tree));
            }
            DrawLose(string.Empty);
            Thread.Sleep(5000);
            gameUI.GamePlataform.Play();
        }
        private static string LoseText => " __        ______        _______. _______      _______      ___      .___  ___.  _______  __  \n|  |      /  __  \\      /       ||   ____|    /  _____|    /   \\     |   \\/   | |   ____||  | \n|  |     |  |  |  |    |   (----`|  |__      |  |  __     /  ^  \\    |  \\  /  | |  |__   |  | \n|  |     |  |  |  |     \\   \\    |   __|     |  | |_ |   /  /_\\  \\   |  |\\/|  | |   __|  |  | \n|  `----.|  `--'  | .----)   |   |  |____    |  |__| |  /  _____  \\  |  |  |  | |  |____ |__| \n|_______| \\______/  |_______/    |_______|    \\______| /__/     \\__\\ |__|  |__| |_______|(__) ";
        private static void DrawLose(string cause) {
            Console.Clear();
            Console.WriteLine($"\n\n\n{LoseText}");
        }

        private static void UpdateView(object sender, UpdateViewArgs moveArgs)
        {

            Console.Clear();
            for (int x = (-1); x < gameUI.GamePlataform.Size.Height + 2; x++)
            {
                for (int y = (-1); y < gameUI.GamePlataform.Size.Width + 2; y++)
                {
                    var px = gameUI.GamePlataform.GetContentInPoint(new Point(x, y));
                    switch (px)
                    {
                        case PointCotent.Null:
                            bool contain = false;
                            foreach (var item in gameUI.GamePlataform.Objects.ToArray())
                            {
                                if (item.Location.Equals(new Point(x, y)))
                                {
                                    switch (item.Type)
                                    {
                                        case ObjectType.Tree:
                                            Console.Write("T");
                                            break;
                                        case ObjectType.Lake:
                                            Console.Write("L");
                                            break;
                                        default:
                                            Console.Write("??");
                                            break;
                                    }
                                    contain = true;
                                }
                            }
                            if (!contain)
                            {
                                Console.Write(".");
                            }
                            break;
                        case PointCotent.Wall:
                            Console.Write("W");
                            break;
                        case PointCotent.Apple:
                            Console.Write("A");
                            break;
                        case PointCotent.SnakeBody:
                            Console.Write("S");
                            break;
                        case PointCotent.SnakeHead:
                            Console.Write("H");
                            break;

                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine($"SL: {gameUI.GamePlataform.Snake.Legacy} CL: {gameUI.GamePlataform.CollectedApples}");
            
        }
    }
}
