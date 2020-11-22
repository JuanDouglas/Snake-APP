using System.Collections.Generic;
using Snake_Logic.Exceptions;
using Snake_Logic.Enums;
using Snake_Logic.Base;
using System;

namespace Snake_Logic
{
    /// <summary>
    /// Cobra do Jogo.
    /// </summary>
    public class Snake
    {
        /// <summary>
        /// Tmanho Atual da cobra.
        /// </summary>
        public Nullable<int> Legacy { get { return Blocks.Count+1; } }
        /// <summary>
        /// Blocos da cobra.
        /// </summary>
        public List<Block> Blocks { get; internal set; }
        /// <summary>
        /// cabeça da cobra.
        /// </summary>
        public Head Head { get; private set; }
        /// <summary>
        /// Plataforma que a cobra está.
        /// </summary>
        public Plataform Plataform { get; set; }

        /// <summary>
        /// Construtor da cobra.
        /// </summary>
        /// <param name="plataform">Plataforma que a cobra irá ficar.</param>
        public Snake(in Plataform plataform, Direction direction, Point location)
        {
            if (plataform.Snake!=null)
            {
                throw new ArgumentException("There is already a snake on this platform.");
            }
            Head = new Head(this,location,direction);
            Blocks = new List<Block>();
            Plataform = plataform ?? throw new ArgumentNullException(nameof(plataform));
        }

        /// <summary>
        /// Adiciona uma virada para a esquerda (X - 1).
        /// </summary>
        public void MoveLeft()
        {
            if (Head.Direction == Direction.Right)
            {
                throw new TurningNotPossibleException("It is impossible to turn in the opposite direction like that.");
            }
            switch (Plataform.GetContentInPoint(new Point(Head.Location.X, Head.Location.Y - 1)))
            {
                case PointCotent.Null:
                    Head.AddTuning(Direction.Left, Head.Location);
                    foreach (var item in Blocks)
                    {
                        item.AddTuning(Direction.Left, Head.Location);
                    }
                    break;
                case PointCotent.Wall:
                    throw new SnakeWallException();
                case PointCotent.Apple:

                    break;
                case PointCotent.SnakeBody:
                    throw new SnakeBodyException();
            }
        }
        /// <summary>
        /// Adiciona uma virada para baixo (X + 1).
        /// </summary>
        public void MoveRight()
        {
            if (Head.Direction == Direction.Left)
            {
                throw new TurningNotPossibleException("It is impossible to turn in the opposite direction like that.");
            }
            switch (Plataform.GetContentInPoint(new Point(Head.Location.X, Head.Location.Y + 1)))
            {
                case PointCotent.Null:
                    Head.AddTuning(Direction.Right, Head.Location);
                    foreach (var item in Blocks)
                    {
                        item.AddTuning(Direction.Right, Head.Location);
                    }
                    break;
                case PointCotent.Wall:
                    throw new SnakeWallException();
                case PointCotent.Apple:

                    break;
                case PointCotent.SnakeBody:
                    throw new SnakeBodyException();
            }
        }

        /// <summary>
        /// Adiciona uma virada para cima (Y - 1).
        /// </summary>
        public void MoveUP()
        {
            if (Head.Direction == Direction.Down)
            {
                throw new TurningNotPossibleException("It is impossible to turn in the opposite direction like that.");
            }
            switch (Plataform.GetContentInPoint(new Point(Head.Location.X - 1, Head.Location.Y)))
            {
                case PointCotent.Null:
                    Head.AddTuning(Direction.UP, Head.Location);
                    foreach (var item in Blocks)
                    {
                        item.AddTuning(Direction.UP, Head.Location);
                    }
                    break;
                case PointCotent.Wall:
                    throw new SnakeWallException();
                case PointCotent.Apple:

                    break;
                case PointCotent.SnakeBody:
                    throw new SnakeBodyException();
            }
        }


        /// <summary>
        /// Adiciona uma virada para baixo (Y - 1).
        /// </summary>
        public void MoveDown()
        {
            if (Head.Direction == Direction.UP)
            {
                throw new TurningNotPossibleException("It is impossible to turn in the opposite direction like that.");
            }
            switch (Plataform.GetContentInPoint(new Point(Head.Location.X + 1, Head.Location.Y )))
            {
                case PointCotent.Null:
                    Head.AddTuning(Direction.Down, Head.Location);
                    foreach (var item in Blocks)
                    {
                        item.AddTuning(Direction.Down, Head.Location);
                    }
                    break;
                case PointCotent.Wall:
                    throw new SnakeWallException();
                case PointCotent.Apple:

                    break;
                case PointCotent.SnakeBody:
                    throw new SnakeBodyException();
            }
        }
        /// <summary>
        /// Faz o movimento do corpo da cobra.
        /// </summary>
        public void Move()
        {
            Head.MoveSnake();
        }

    }
}
