using System.Linq;
using Snake_Logic.Exceptions;
using Snake_Logic.Enums;
using Snake_Logic.Base;
using System.Collections.Generic;

namespace Snake_Logic
{
    public class Head : Block
    {
        /// <summary>
        /// Cobra.
        /// </summary>
        public Snake Snake { get; private set; }
        private Point location;
        /// <summary>
        /// Construtor da cabeça ("Head").
        /// </summary>
        /// <param name="snake">Cobra "dona" da cabeça.</param>
        /// <param name="location">Local incial</param>
        /// <param name="direction">Direção Incial</param>
        public Head(Snake snake, Point locatin, Direction direction) : base(locatin, direction, 0)
        {
            Snake = snake;
            location = locatin;
        }
        
        /// <summary>
        /// Move a cobra.
        /// </summary>
        internal void MoveSnake() {
            Point point = location;
            var turning = Turnings.FirstOrDefault(fs => fs.Index == 1);
            if (turning != null)
            {
                if (turning.Location.Equals(Location))
                {
                    Direction = turning.Direction;
                }
            }
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
                case PointCotent.Null:
                    base.Move();
                    foreach (var item in Snake.Blocks)
                    {
                        item.Move();
                    }
                    break;
                case PointCotent.Wall:
                    throw new SnakeWallException();
                case PointCotent.Apple:
                    base.Move();
                    Apple apple = Snake.Plataform.GetApple(point);
                    Snake.Plataform.CollectAppleInvoke(apple,new Event_Args.CollectAppleArgs(apple,apple.Power,Snake));
                    foreach (var item in Snake.Blocks)
                    {
                        item.Move();
                    }
                    break;
                case PointCotent.SnakeBody:
                    throw new SnakeBodyException();
            }
        }

    }
}
