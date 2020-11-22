using System.Linq;
using Snake_Logic.Exceptions;
using Snake_Logic.Enums;
using Snake_Logic.Base;

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
        public Head(in Snake snake, Point locatin, Direction direction) : base(locatin, direction, 0)
        {
            Snake = snake;
            location = locatin;
        }
        
        /// <summary>
        /// Move a cobra.
        /// </summary>
        internal void MoveSnake() {
            Point point = location;
            Point previosPoint = new Point();
            int index = GetBlockIndex();
            Direction previosDirection;
            Block previos;

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
                    Apple apple = Snake.Plataform.GetApple(point);
                    Snake.Plataform.CollectAppleInvoke(apple,new Event_Args.CollectAppleArgs(apple,apple.Power,Snake));

                    for (int i = 0; i < apple.Power; i++)
                    {
                        previos = Snake.Blocks.FirstOrDefault(fs => fs.Index.Value == index);
                        if (previos == null)
                        {
                            previos = Snake.Head;
                        }
                        previosDirection = previos.Direction;
                        switch (previosDirection)
                        {
                            case Direction.Down:
                                previosPoint = new Point(previos.Location.X-1,previos.Location.Y);
                                break;
                            case Direction.UP:
                                previosPoint = new Point(previos.Location.X + 1, previos.Location.Y);
                                break;
                            case Direction.Left:
                                previosPoint = new Point(previos.Location.X, previos.Location.Y+1);
                                break;
                            case Direction.Right:
                                previosPoint = new Point(previos.Location.X, previos.Location.Y-1);
                                break;
                        }
                        Snake.Blocks.Add(new Block(previosPoint,previos.Direction,previos.Turnings,index));
                    }
                    base.Move();
                    foreach (var item in Snake.Blocks)
                    {
                        item.Move();
                    }
                    break;
                case PointCotent.SnakeBody:
                    throw new SnakeBodyException();
            }
        }
        /// <summary>
        /// Obtém o índicie do proxímo bloco.
        /// </summary>
        /// <returns>Retorna o valor do índicie.</returns>
        private int GetBlockIndex() {
            int max = 1;
            foreach (var item in Snake.Blocks)
            {
                if (item.Index > max)
                {
                    max = item.Index.Value;
                }
            }
            return max;
        }
    }
}
