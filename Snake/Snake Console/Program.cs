using Snake.Logic;
using Snake.Logic.Base;
using Snake.Logic.Enums;
using Snake.Logic.EventArgs;
using Snake.Logic.Graphic;
using Snake.Logic.Graphic.Base;
using System;
using System.Threading;
using System.Timers;

namespace Snake.Console
{
    using Console = System.Console;
    using Timer = System.Timers.Timer;

    class Program
    {
        static GraphicGamePlataform plataform => gameUI.GamePlataform;
        static GameUI gameUI;
        static int Width = 10;
        static int Height = 10;
        static int Velocity = 750;
        static Random rd;
        static Timer tm;
        static void Main(string[] args)
        {
            rd = new Random();
            gameUI = new GameUI(null, 800, 600)
            {
                GamePlataform = new GraphicGamePlataform(Width, Height, Velocity)
            };
            gameUI.GamePlataform.LoseGame += new GamePlataform.LoseGameHandler(Lose);

            for (int i = 0; i < rd.Next(0, Width / 2); i++)
            {
                gameUI.GamePlataform.AddObject(new DefaultObject(gameUI.GamePlataform.Size, new Point(rd.Next(2, Width), rd.Next(2, Width)), ObjectContent.Solid, ObjectType.Tree));
            }
            tm = new Timer(1000);
            tm.Elapsed += new ElapsedEventHandler(Refresh);
            tm.Start();
            gameUI.GamePlataform.Play();

            char consoleKey;
            do
            {
                consoleKey = (char)Console.Read();
                try
                {
                    if (consoleKey == 0x18)
                    {
                        gameUI.GamePlataform.Snake.MoveUP();
                    }
                    if (consoleKey== 0x19)
                    {
                        gameUI.GamePlataform.Snake.MoveDown();
                    }
                    if (consoleKey  == 0x1B)
                    {
                        gameUI.GamePlataform.Snake.MoveLeft();
                    }
                    if (consoleKey == 0x1A)
                    {
                        gameUI.GamePlataform.Snake.MoveRight();
                    }
                    if (consoleKey == 0x6B)
                    {
                        gameUI.GamePlataform.Snake.Kill(KillCause.User);
                    }
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("Ei!");
                }
            } while (consoleKey != 0x1B);
        }

        private static void Refresh(object sender, ElapsedEventArgs args)
        {
            gameUI.Draw().Save($"{Environment.CurrentDirectory}\\OutPut.jpeg");
        }

        private static void Lose(object sender, LoseGameArgs LoseArg)
        {
            gameUI.GamePlataform = new GraphicGamePlataform(Width, Height, Velocity);
            gameUI.GamePlataform.UpdateView += new GamePlataform.UpdateViewHandler(UpdateView);
            gameUI.GamePlataform.LoseGame += new GamePlataform.LoseGameHandler(Lose);
            for (int i = 0; i < rd.Next(0, Width / 2); i++)
            {
                gameUI.GamePlataform.AddObject(new DefaultObject(plataform.Size, new Point(rd.Next(2, Width), rd.Next(2, Width)), ObjectContent.Solid, ObjectType.Tree));
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
