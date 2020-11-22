using Snake_Logic.Enums;

namespace Snake_Logic.Base
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
