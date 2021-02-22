﻿using Snake.Logic.Base;
using Snake.Logic.Base.Interfaces;
using Snake.Logic.Enums;
using System;

namespace Snake.Logic.Event_Args
{
    public class ObjectInteractionArgs
    {
        public ObjectType Type { get { return this.Object.Type; } }
        public Point Location { get { return this.Object.Location; } }
        public IPlataformObject Object { get; set; }
        public Snake Snake { get; set; }

        public ObjectInteractionArgs(IPlataformObject @object, Snake snake)
        {
            Object = @object ?? throw new ArgumentNullException(nameof(@object));
            Snake = snake ?? throw new ArgumentNullException(nameof(snake));
        }
    }
}
