using Snake.Logic.Base;
using Snake.Logic.Enums;
using Snake.Logic.Event_Args;
using System;
using System.Collections.Generic;

namespace Snake.Logic
{
    /// <summary>
    /// Cobra do Jogo.
    /// </summary>
    public class Snake : PlataformObject
    {
        /// <summary>
        /// Tmanho Atual da cobra.
        /// </summary>
        public Nullable<int> Legacy { get { return Blocks.Count + 1; } }
        /// <summary>
        /// Blocos da cobra.
        /// </summary>
        public List<SnakeBlock> Blocks { get; internal set; }
        /// <summary>
        /// cabeça da cobra.
        /// </summary>
        public Head Head { get; private set; }
        /// <summary>
        /// Plataforma que a cobra está.
        /// </summary>
        public Plataform Plataform { get; set; }

        public delegate void SnakeUpgradeHandler(object sender, SnakeUpgradeArgs args);
        public event SnakeUpgradeHandler SnakeUpprade;

        /// <summary>
        /// Construtor da cobra.
        /// </summary>
        /// <param name="plataform">Plataforma que a cobra irá ficar.</param>
        public Snake(Plataform plataform, Direction direction, Point location) : base(location, ObjectContent.Solid, ObjectType.Snake)
        {
            if (plataform.Snake != null)
            {
                throw new ArgumentException("There is already a snake on this platform.");
            }
            Plataform = plataform ?? throw new ArgumentNullException(nameof(plataform));
            Head = new Head(this, location, direction);
            Blocks = new List<SnakeBlock>();
            SnakeUpprade += new SnakeUpgradeHandler((object sender, SnakeUpgradeArgs args) =>
            {
                _ = args;
            });
        }

        /// <summary>
        /// Adiciona uma virada para a esquerda (X - 1).
        /// </summary>
        public void MoveLeft()
        {
            Point location = Head.Location;
            Head.AddTuning(Direction.Left, location);
            foreach (var item in Blocks)
            {
                item.AddTuning(Direction.Left, location);
            }
        }
        /// <summary>
        /// Adiciona uma virada para baixo (X + 1).
        /// </summary>
        public void MoveRight()
        {
            Point location = Head.Location;
            Head.AddTuning(Direction.Right, location);
            foreach (var item in Blocks)
            {
                item.AddTuning(Direction.Right, location);
            }
        }

        /// <summary>
        /// Adiciona uma virada para cima (Y + 1).
        /// </summary>
        public void MoveUP()
        {
            Point location = Head.Location;
            Head.AddTuning(Direction.UP, location);
            foreach (var item in Blocks)
            {
                item.AddTuning(Direction.UP, location);
            }
        }


        /// <summary>
        /// Adiciona uma virada para baixo (Y - 1).
        /// </summary>
        public void MoveDown()
        {
            Point location = Head.Location;
            Head.AddTuning(Direction.Down, location);
            foreach (var item in Blocks)
            {
                item.AddTuning(Direction.Down, location);
            }
        }
        /// <summary>
        /// Faz o movimento do corpo da cobra.
        /// </summary>
        public void Move()
        {
            Head.MoveSnake();
        }

        protected internal void SnakeUpgradeInvoke(object sender, SnakeUpgradeArgs args)
        {
            SnakeUpprade.Invoke(sender, args);
        }
    }
}
