using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snake.Logic;
using Snake.Logic.Base;
using Snake.Logic.Enums;
using Snake.Logic.Event_Args;
using Snake.Logic.Graphic;

namespace Snake_Console
{
    class Program
    {
        static Plataform plataform;
        static int Width = 10;
        static int Height = 10;
        static int Velocity = 1500;
        static Random rd;
        static void Main(string[] args)
        {
            rd = new Random();
            plataform = new Plataform(Width,Height,Velocity);
            plataform.MoveSnakeEvent += new Plataform.MoveSnakeEventHandler(MoveSnake);
            plataform.LoseGame += new Plataform.LoseGameHandler(Lose);
            for (int i = 0; i < rd.Next(0,Width/2); i++)
            {
                plataform.Objects.Add(new PlataformObject(new Point(rd.Next(2, Width), rd.Next(2, Width)), ObjectContent.Solid, ObjectType.Tree));
            }
            Background background= new Background(in plataform,800,800);
            background.GetImage().Save($"{Environment.CurrentDirectory}\\Teste.jpeg");
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
            } while (consoleKey.Key!=ConsoleKey.Escape);
        }
        private static void Lose(object sender, LoseGameArgs LoseArg) {
            plataform.Pause();
            plataform = new Plataform(Width, Height, Velocity);
            plataform.MoveSnakeEvent += new Plataform.MoveSnakeEventHandler(MoveSnake);
            plataform.LoseGame += new Plataform.LoseGameHandler(Lose);
            for (int i = 0; i < rd.Next(0, Width / 2); i++)
            {
                plataform.Objects.Add(new PlataformObject(new Point(rd.Next(2, Width), rd.Next(2, Width)), ObjectContent.Solid, ObjectType.Tree));
            }
            plataform.Play();
        }
        private static void MoveSnake(object sendeer, MoveSnakeArgs moveArgs) {
            Console.Clear();
            for (int x = (-1); x < plataform.Height + 2; x++)
            {
                for (int y = (-1); y < plataform.Width + 2; y++)
                {
                    var px = plataform.GetContentInPoint(new Point(x, y));
                    switch (px)
                    {
                        case PointCotent.Null:
                            bool contain = false;
                            foreach (var item in plataform.Objects)
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
