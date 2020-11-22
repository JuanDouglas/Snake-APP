using Snake_Logic.Base;
using Snake_Logic.Enums;
using System;

namespace Snake_Logic.Event_Args
{
    public class ObjectInteractionArgs
    {
        public ObjectType Type { get { return this.Object.Type; } }
        public Point Location { get { return this.Object.Location; } }
        public PlataformObject Object { get; set; }
        public Snake Snake { get; set; }

        public ObjectInteractionArgs(PlataformObject @object, Snake snake)
        {
            Object = @object ?? throw new ArgumentNullException(nameof(@object));
            Snake = snake ?? throw new ArgumentNullException(nameof(snake));
        }
    }
}
