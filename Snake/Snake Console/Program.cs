using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        static GraphicGamePlataform plataform;
        static GameUI gameUI;
        static int Width = 10;
        static int Height = 10;
        static int Velocity = 750;
        static Random rd;
        static void Main(string[] args)
        {
            rd = new Random();
            plataform = new GraphicGamePlataform(Width, Height, Velocity);
            plataform.MoveSnakeEvent += new GamePlataform.MoveSnakeEventHandler(MoveSnake);
            plataform.LoseGame += new GamePlataform.LoseGameHandler(Lose);
            for (int i = 0; i < rd.Next(0, Width / 2); i++)
            {
                plataform.Objects.Add(new DefaultObject(plataform.Size, new Point(rd.Next(2, Width), rd.Next(2, Width)), ObjectContent.Solid, ObjectType.Tree));
            }
            GameUI gameUI = new GameUI(plataform, 800, 600);
            gameUI.NewViewCreate += new GameUI.NewViewCreateHandler((object sender, NewViewCreateEventArgs eventArgs) =>
            {
                eventArgs.Result.Save($"{Environment.CurrentDirectory}\\OutPut.jpeg");
            });
            plataform.Play();
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
            } while (consoleKey.Key != ConsoleKey.Escape);
        }
        private static void Lose(object sender, LoseGameArgs LoseArg)
        {
            plataform.Pause();
            plataform = new GraphicGamePlataform(Width, Height, Velocity);
            plataform.MoveSnakeEvent += new GamePlataform.MoveSnakeEventHandler(MoveSnake);
            plataform.LoseGame += new GamePlataform.LoseGameHandler(Lose);
            //for (int i = 0; i < rd.Next(0, Width / 2); i++)
            //{
            //    plataform.Objects.Add(new PlataformObject(new Point(rd.Next(2, Width), rd.Next(2, Width)), ObjectContent.Solid, ObjectType.Tree));
            //}
            plataform.Play();
        }
        private static void MoveSnake(object sendeer, MoveSnakeArgs moveArgs)
        {

            Console.Clear();
            for (int x = (-1); x < plataform.Size.Height + 2; x++)
            {
                for (int y = (-1); y < plataform.Size.Width + 2; y++)
                {
                    var px = plataform.GetContentInPoint(new Point(x, y));
                    switch (px)
                    {
                        case PointCotent.Null:
                            bool contain = false;
                            foreach (var item in plataform.Objects.ToArray())
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
        }
    }
}
