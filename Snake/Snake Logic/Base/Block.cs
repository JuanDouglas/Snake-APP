using System;
using System.Collections.Generic;
using Snake_Logic.Enums;
using System.Linq;

namespace Snake_Logic.Base
{
    /// <summary>
    /// Bloco da cobra.
    /// </summary>
    public partial class Block
    {
        /// <summary>
        /// Posição atual do Bloco.
        /// </summary>
        public Point Location { get; protected internal set; }
        /// <summary>
        /// Direção atual do Bloco.
        /// </summary>
        public Direction Direction { get; protected internal set; }
        /// <summary>
        /// Viradas que serão feitas.
        /// </summary>
        public List<Turning> Turnings { get; protected internal set; }
        /// <summary>
        /// Posição no índicie.
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// Construtor de blocos secundários.
        /// </summary>
        /// <param name="location">Local inicial.</param>
        /// <param name="direction">Direção inicial.</param>
        /// <param name="turnings">Viradas adicionadas ao bloco de anterior (índicie - 1).</param>
        /// <param name="index">Numéro no índicie.</param>
        public Block(Point location, Direction direction, List<Turning> turnings, int index)
        {
            Location = location;
            Direction = direction;
            Turnings = turnings ?? throw new ArgumentNullException(nameof(turnings));
            Index = index;
        }
        /// <summary>
        /// Construtor de bloco inicial.
        /// </summary>
        /// <param name="location">Local inicial.</param>
        /// <param name="direction">Direção inicial.</param>
        /// <param name="index">Numéro no índicie.</param>
        public Block(Point location, Direction direction, int index)
        {
            Location = location;
            Direction = direction;
            Index = index;
            Turnings = new List<Turning>();
        }
        /// <summary>
        /// Faz o movimento deste bloco de acordo com a direção, caso tenha uma curva ("Turning") adiciona faz a curva e muda a direção.
        /// </summary>
        protected internal virtual void Move()
        {
            var turning = Turnings.FirstOrDefault(fs => fs.Index == 1);
            if (turning != null)
            {
                if (turning.Location.Equals(Location))
                {
                    Direction = turning.Direction;
                    Turnings.RemoveAll(fs => fs.Index == 1);
                    foreach (var item in Turnings)
                    {
                        item.Index--;
                    }
                }
            }
            switch (Direction)
            {
                case Direction.Down:
                    Location = new Point(Location.X, Location.Y + 1);
                    break;
                case Direction.UP:
                    Location = new Point(Location.X, Location.Y - 1);
                    break;
                case Direction.Left:
                    Location = new Point(Location.X - 1, Location.Y);
                    break;
                case Direction.Right:
                    Location = new Point(Location.X + 1, Location.Y);
                    break;
            }
        }
        /// <summary>
        /// Adiciona uma nova curva a lista de curvas.
        /// </summary>
        /// <param name="direction">Direção desta curva</param>
        /// <param name="location">Local da curva</param>
        public void AddTuning(Direction direction, Point location)
        {
            Turnings.Add(new Turning(direction, location, Turnings.Count + 1));
        }
    }
}
