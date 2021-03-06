﻿using Snake.Logic.Base;
using Snake.Logic.Enums;
using Snake.Logic.EventArgs;
using System.Linq;

namespace Snake.Logic
{
    public class Head : SnakeBlock
    {
        /// <summary>
        /// Cobra.
        /// </summary>
        public Snake Snake { get; private set; }
        /// <summary>
        /// Construtor da cabeça ("Head").
        /// </summary>
        /// <param name="snake">Cobra "dona" da cabeça.</param>
        /// <param name="location">Local incial</param>
        /// <param name="direction">Direção Incial</param>
        public Head(in Snake snake, Point location, Direction direction) : 
            base(snake.Plataform, location, direction, 0) => Snake = snake;
        /// <summary>
        /// Move a cobra.
        /// </summary>
        internal void MoveSnake()
        {
            Point point = Location;
            var turning = Turnings.FirstOrDefault(fs => fs.Location.Equals(Location));

            if (turning != null)
            {
                if (turning.Location.Equals(Location))
                {
                    Direction = turning.Direction;
                }
            }

            Plataform.MoveSnakeInvoke(Snake,new MoveSnakeArgs());
            switch (Direction)
            {
                case Direction.Down:
                    point = new Point(Location.X + 1, Location.Y);
                    break;
                case Direction.UP:
                    point = new Point(Location.X - 1, Location.Y);
                    break;
                case Direction.Left:
                    point = new Point(Location.X, Location.Y - 1);
                    break;
                case Direction.Right:
                    point = new Point(Location.X, Location.Y + 1);
                    break;
            }
       
            switch (Snake.Plataform.GetContentInPoint(point))
            {
                case PointCotent.Wall:
                    Plataform.LoseInvoke(this, new LoseGameArgs(null, "It is not possible to go through the wall.", KillCause.Wall, Snake.Legacy, Plataform.CollectedApples));
                    break;
                case PointCotent.Apple:
                    base.Move();
                    Apple apple = Snake.Plataform.GetApple(point);
                    Snake.SnakeUpgradeInvoke(apple, new SnakeUpgradeArgs(apple, Snake.Legacy.Value, Snake.Legacy.Value + apple.Power, Snake));
                    Snake.Plataform.CollectAppleInvoke(apple, new CollectAppleArgs(apple, apple.Power, Snake));
                    foreach (var item in Snake.Blocks)
                    {
                        item.Move();
                    }
                    break;
                case PointCotent.SnakeBody:
                    Plataform.LoseInvoke(this, new LoseGameArgs(null, "It is not possible that the snake can cross its body.", KillCause.SnakeBody, Snake.Legacy, Plataform.CollectedApples));
                    break;
                default:
                    foreach (var item in Plataform.Objects.ToArray())
                    {
                        if (item.Location.Equals(point))
                        {
                            if (item.Content == ObjectContent.Solid)
                            {
                                Plataform.LoseInvoke(this, new LoseGameArgs(null, "The snake encountered an obstacle in front of you.", KillCause.SolidObject, Snake.Legacy, Plataform.CollectedApples));
                            }
                            Plataform.ObjectInteractionInvoke(Plataform.Snake, new ObjectInteractionArgs(item, Plataform.Snake));
                        }
                    }
                    base.Move();
                    foreach (var item in Snake.Blocks)
                    {
                        item.Move();
                    }
                    break;
            }
            
        }

    }
}
