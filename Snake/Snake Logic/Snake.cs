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
        public Snake(Plataform plataform, Direction direction, Point location)
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
            Head.AddTuning(Direction.Left, Head.Location);
            foreach (var item in Blocks)
            {
                item.AddTuning(Direction.Left, Head.Location);
            }
        }
        /// <summary>
        /// Adiciona uma virada para baixo (X + 1).
        /// </summary>
        public void MoveRight()
        {
            Head.AddTuning(Direction.Right, Head.Location);
            foreach (var item in Blocks)
            {
                item.AddTuning(Direction.Right, Head.Location);
            }
        }

        /// <summary>
        /// Adiciona uma virada para cima (Y - 1).
        /// </summary>
        public void MoveUP()
        {
            Head.AddTuning(Direction.UP, Head.Location);
            foreach (var item in Blocks)
            {
                item.AddTuning(Direction.UP, Head.Location);
            }
        }


        /// <summary>
        /// Adiciona uma virada para baixo (Y - 1).
        /// </summary>
        public void MoveDown()
        {
            Head.AddTuning(Direction.Down, Head.Location);
            foreach (var item in Blocks)
            {
                item.AddTuning(Direction.Down, Head.Location);
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
