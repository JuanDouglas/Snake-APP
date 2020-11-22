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
        /// <summary>
        /// Construtor da cabeça ("Head").
        /// </summary>
        /// <param name="snake">Cobra "dona" da cabeça.</param>
        /// <param name="location">Local incial</param>
        /// <param name="direction">Direção Incial</param>
        public Head(in Snake snake, Point location, Direction direction) : base(location, direction, 0)
        {
            Snake = snake;
        }
        
        /// <summary>
        /// Move a cobra.
        /// </summary>
        internal void MoveSnake() {
            Point point = new Point(0, 0);
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
                    int index = GetBlockIndex();
                    for (int i = 0; i < apple.Power; i++)
                    {
                        Block previos = Snake.Blocks.First(fs=>fs.Index==index-1);
                        Snake.Blocks.Add(new Block(previos.Location,previos.Direction,previos.Turnings,index));
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
            int max = 0;
            foreach (var item in Snake.Blocks)
            {
                if (item.Index > max)
                {
                    max = item.Index;
                }
            }
            return max;
        }
    }
}
