using System;

namespace Snake.Logic.Event_Args
{
    public class SnakeUpgradeArgs
    {
        public object Cause { get; set; }
        public int LegacyPrevios { get; set; }
        public int LegacyBefore { get; set; }
        public Snake Snake { get; set; }

        public SnakeUpgradeArgs(object cause, int legacyPrevios, int legacyBefore, Snake snake)
        {
            Cause = cause ?? throw new ArgumentNullException(nameof(cause));
            LegacyPrevios = legacyPrevios;
            LegacyBefore = legacyBefore;
            Snake = snake ?? throw new ArgumentNullException(nameof(snake));
        }
    }
}
