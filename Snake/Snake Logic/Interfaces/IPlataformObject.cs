using Snake.Logic.Enums;
using System;

namespace Snake.Logic.Base.Interfaces
{
    public interface IPlataformObject 
    {
        Point Location { get; set; }
        ObjectContent Content { get; set; }
        ObjectType Type { get; set; }
        Guid ID { get; set; }
        Size PlataformSize { get; set; }
    }
}