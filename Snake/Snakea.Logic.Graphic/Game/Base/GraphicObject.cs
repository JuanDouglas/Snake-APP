using Snake.Logic.Base;
using Snake.Logic.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Point = Snake.Logic.Base.Point;
using Size = Snake.Logic.Base.Size;

namespace Snake.Logic.Graphic.Game.Base
{
    public class GraphicObject : PlataformObject
    {
        public Image View { get; set; }
        public GraphicObject(Image defaultView, PlataformObject father) : this(defaultView, father.Location, father.Content, father.Type)
        {
            ID = father.ID;
        }
        public GraphicObject(Image defaultView, Point location, ObjectContent content, ObjectType type) : base(new Size(),location, content, type)
        {
            View = defaultView;
        }
        public Image Draw()
        {
            return View;
        }
        public Image Draw(int width,int height)
        {
            return View;
        }
    }
}
