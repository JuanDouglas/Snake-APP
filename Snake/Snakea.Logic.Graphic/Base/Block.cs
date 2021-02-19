using System.Drawing;

namespace Snake.Logic.Graphic.Base
{
    public class Block
    {
        public Point Axis { get; set; }
        public Point PlataformAxis { get; set; }
        public Block(Point axis, Point plataformAxis)
        {
            Axis = axis;
            PlataformAxis = plataformAxis;
        }
    }
}