using System;

namespace Snake_Logic.Event_Args
{
    public class CollectAppleArgs
    {
        public Apple Apple { get; set; }
        public int Power { get; set; }
        public Snake Snake { get; set; }
        public Plataform Plataform { get => Snake.Plataform; }

        public CollectAppleArgs(Apple apple, int power, Snake snake)
        {
            Apple = apple ?? throw new ArgumentNullException(nameof(apple));
            Power = power;
            Snake = snake ?? throw new ArgumentNullException(nameof(snake));
        }
    }
}
