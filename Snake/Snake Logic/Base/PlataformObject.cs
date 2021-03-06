﻿using Snake.Logic.Base.Interfaces;
using Snake.Logic.Enums;
using System;


namespace Snake.Logic.Base
{
    public abstract class PlataformObject : IPlataformObject
    {
        public Point Location { get; set; }
        public ObjectContent Content { get; set; }
        public ObjectType Type { get; set; }
        public Guid ID { get; set; }
        public Size PlataformSize { get; set; }

        public const ObjectContent DefaultContent = ObjectContent.Not_Solid;
        public const ObjectType DefaultType = ObjectType.None;

        public PlataformObject(in Size plataformSize) : this(plataformSize, new Point()) { }
        public PlataformObject(in Size plataformSize, Point point) : this(plataformSize, point, DefaultContent, DefaultType) { }
        public PlataformObject(in Size plataformSize, Point location, ObjectContent content, ObjectType type)
        {
            Location = location;
            Content = content;
            Type = type;
            PlataformSize = plataformSize;
            ID = Guid.NewGuid();
        }

        public override bool Equals(object obj)
        {
            if (obj is PlataformObject)
            {
                if (ID.Equals(((PlataformObject)obj).ID))
                {
                    return true;
                }
            }
            return base.Equals(obj);
        }


    }

}
