using Snake.Logic.Enums;
using System;

namespace Snake.Logic.EventArgs
{
    public class LoseGameArgs
    {
        public object CauseObject { get; set; }
        public string Justification { get; set; }
        public KillCause Cause { get; set; }
        public Nullable<int> Points { get; set; }
        public Nullable<int> CollectedApples { get; set; }

        public LoseGameArgs(object causeObject, string justification, KillCause cause, int? points, int? collectedApples)
        {
            CauseObject = causeObject;
            Justification = justification ?? throw new ArgumentNullException(nameof(justification));
            Cause = cause;
            Points = points;
            CollectedApples = collectedApples;
        }
    }
}
