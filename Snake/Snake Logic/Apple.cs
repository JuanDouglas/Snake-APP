using Snake.Logic.Base;

namespace Snake.Logic
{
    /// <summary>
    /// Maçâ do jogo.
    /// </summary>
    public class Apple : PlataformObject
    {
        /// <summary>
        /// "Poder" (quanto blocos irá adicionar) da cobra.
        /// </summary>
        public int Power { get; set; }
        public int AppleDeacreaseSpeed { get; set; }

        /// <summary>
        /// Construtor da Maçã.
        /// </summary>
        /// <param name="location">Local da Maçã.</param>
        /// <param name="power">"Poder" da Maçã.</param>
        public Apple(Point location, int power, int speed_deacrease) : base(location, Enums.ObjectContent.Not_Solid,Enums.ObjectType.)
        {
            Location = location;
            AppleDeacreaseSpeed = speed_deacrease;
            Power = power;
        }
    }
}
