using Snake.Logic.Base;
using Snake.Logic.Enums;
using Snake.Logic.Graphic.Base.Interfaces;
using Snake.Logic.Graphic.EventArgs;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Point = Snake.Logic.Base.Point;

namespace Snake.Logic.Graphic.Base
{
    public class GraphicObject : PlataformObject, IGraphicObject
    {
        public Image ViewContent { get; set; }
        public GraphicObject(Point location, ObjectContent content, ObjectType type) : this(location, content, type, new Bitmap(0,0))
        {

        }
        public GraphicObject(Point location, ObjectContent content, ObjectType type, Image viewContent) : base(location, content, type)
        {
            ViewContent = viewContent;
        }

        public event DrawingHandler Drawing;
        public event FinishedDrawingHandler FinishDrawing;
        public Image Draw()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Drawing.Invoke(this,new DrawingEventArgs());
            FinishDrawing.Invoke(this, new FinishedDrawingArgs(ViewContent,stopwatch.Elapsed));
            stopwatch.Stop();
            return ViewContent;
           
        }

        public Point GetCenterPoint()
        {
            return this.Location;
        }
    }
   
}
