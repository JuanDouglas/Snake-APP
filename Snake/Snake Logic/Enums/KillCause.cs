using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Logic.Enums
{
   public enum KillCause :uint
    {
        Wall,
        SnakeBody,
        SolidObject,
        User,
        NotSpecified
    }
}
