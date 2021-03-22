using Snake.Logic.Base;
using Snake.Logic.Enums;

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
        public Apple(in Size plataformSize,Point location, int power, int speed_deacrease) : base(plataformSize,location, ObjectContent.Not_Solid,ObjectType.Apple)
        {
            Location = location;
            AppleDeacreaseSpeed = speed_deacrease;
            Power = power;
        }
    }
}
