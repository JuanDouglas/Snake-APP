using Snake.Logic.Enums;

namespace Snake.Logic.Base
{
    public class PlataformObject
    {
        public Point Location { get; set; }
        public ObjectContent Content { get; set; }
        public ObjectType Type { get; set; }

        public PlataformObject(Point location, ObjectContent content, ObjectType type)
        {
            Location = location;
            Content = content;
            Type = type;
        }
    }
}
