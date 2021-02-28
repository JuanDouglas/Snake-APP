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
using Size = Snake.Logic.Base.Size;
namespace Snake.Logic.Graphic.Base
{
    public class GraphicObject : PlataformObject, IGraphicObject
    {
        public Image ViewContent { get; set; }
        public bool isVisible { get; set; }
        public GraphicObject(Size plataformSize, Point location, ObjectContent content, ObjectType type) : this(plataformSize, location, content, type, new Bitmap(0, 0))
        {

        }
        public GraphicObject(Size plataformSize, Point location, ObjectContent content, ObjectType type, Image viewContent) : base(plataformSize, location, content, type)
        {
            ViewContent = viewContent;
        }

        public event DrawingHandler Drawing;
        public event FinishedDrawingHandler FinishDrawing;
        public DrawResult Draw(Size uiSize)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Drawing.Invoke(this, new DrawingEventArgs());

            DrawResult drawResult = new DrawResult(ViewContent, Location);

            stopwatch.Stop();

            FinishDrawing.Invoke(this, new FinishedDrawingArgs(ViewContent, stopwatch.Elapsed));
            drawResult.Elapsed = stopwatch.Elapsed;

            return drawResult;
        }

        public DrawResult Draw(Size uiSize, Size maxSize)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            DrawResult drawResult = Draw(uiSize);
            if (drawResult.Image.Width > maxSize.Width || drawResult.Image.Height > maxSize.Height)
            {
                drawResult.Image = new Bitmap(drawResult.Image, new System.Drawing.Size(maxSize.Width, maxSize.Height));
            }
            stopwatch.Stop();
            drawResult.Elapsed += stopwatch.Elapsed;
            return drawResult;
        }
    }

}
