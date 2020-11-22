using Snake_Logic.Base;
using Snake_Logic.Enums;
using Snake_Logic.Event_Args;
using Snake_Logic.Exceptions;
using System.Linq;

namespace Snake_Logic
{
    public class Head : Block
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
        public Head(in Snake snake, Point location, Direction direction) : base(snake.Plataform, location, direction, 0)
        {
            Snake = snake;
        }

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
                    Plataform.LoseInvoke(this,new LoseGameArgs(PointCotent.Wall,Plataform.Snake.Legacy,Plataform.CollectedApples));
                    break;
                case PointCotent.Apple:
                    base.Move();
                    Apple apple = Snake.Plataform.GetApple(point);
                    Snake.SnakeUpgradeInvoke(apple, new SnakeUpgradeArgs(apple,Snake.Legacy.Value,Snake.Legacy.Value+apple.Power,Snake));
                    Snake.Plataform.CollectAppleInvoke(apple, new CollectAppleArgs(apple, apple.Power, Snake));
                    foreach (var item in Snake.Blocks)
                    {
                        item.Move();
                    }
                    break;
                case PointCotent.SnakeBody:
                    throw new SnakeBodyException();
                default:
                    foreach (var item in Plataform.Objects)
                    {
                        if (item.Location.Equals(point))
                        {
                            if (item.Content == ObjectContent.Solid)
                            {
                                Plataform.LoseInvoke(Plataform.Snake, new LoseGameArgs(item, Plataform.Snake.Legacy, Plataform.CollectedApples));
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
