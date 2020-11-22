using Snake_Logic.Enums;

namespace Snake_Logic.Base
{
    /// <summary>
    /// Virada da cobra
    /// </summary>
    public class Turning
    {
        /// <summary>
        /// Direção da curva
        /// </summary>
        public Direction Direction { get; set; }
        /// <summary>
        /// Local da curva
        /// </summary>
        public Point Location { get; set; }
        /// <summary>
        /// Índicie da curva.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Construtor da curva.
        /// </summary>
        /// <param name="direction">Direção da curva.</param>
        /// <param name="location">Local da curva.</param>
        /// <param name="index">Índicie da curva.</param>
        public Turning(Direction direction, Point location, int index)
        {
            Direction = direction;
            Location = location;
            Index = index;
        }
    }
}
