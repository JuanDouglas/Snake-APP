using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Logic.Event_Args
{
    public class LoseGameArgs
    {
        public object Cause { get; set; }
        public Nullable<int> Points { get; set; }
        public Nullable<int> CollectedApples { get; set; }

        public LoseGameArgs(object cause, Nullable<int> points, Nullable<int> collectedApples)
        {
            Cause = cause ?? throw new ArgumentNullException(nameof(cause));
            Points = points;
            CollectedApples = collectedApples;
        }
    }
}
