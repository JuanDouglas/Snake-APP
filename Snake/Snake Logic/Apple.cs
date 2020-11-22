using Snake_Logic.Base;

namespace Snake_Logic
{
    /// <summary>
    /// Maçâ do jogo.
    /// </summary>
    public class Apple
    {
        /// <summary>
        /// Localização da Maçã.
        /// </summary>
        public Point Location { get; set; }
        /// <summary>
        /// "Poder" (quanto blocos irá adicionar) da cobra.
        /// </summary>
        public int Power { get; set; }

        /// <summary>
        /// Construtor da Maçã.
        /// </summary>
        /// <param name="location">Local da Maçã.</param>
        /// <param name="power">"Poder" da Maçã.</param>
        public Apple(Point location, int power)
        {
            Location = location;
            Power = power;
        }
    }
}
