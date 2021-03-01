using Snake.Logic.Base;
using Snake.Logic.Base.Interfaces;
using Snake.Logic.Enums;
using Snake.Logic.Graphic.Base.Interfaces;
using Snake.Logic.Graphic.EventArgs;
using System.Diagnostics;
using System.Drawing;
using Point = Snake.Logic.Base.Point;
using Size = Snake.Logic.Base.Size;

namespace Snake.Logic.Graphic.Base
{
    public class GraphicObject : Logic.Base.PlataformObject, IGraphicObject
    {
        public Image ViewContent { get; set; }
        public bool isVisible { get; set; }
        public int UpdateVersion { get; set; }

        public GraphicObject(PlataformObject plataformObject) : this( plataformObject.PlataformSize, plataformObject.Location,plataformObject.Content,plataformObject.Type) { 
        
        }
        public GraphicObject(IPlataformObject plataformObject) : this(plataformObject.PlataformSize, plataformObject.Location, plataformObject.Content, plataformObject.Type)
        {

        }
        public GraphicObject(Size plataformSize, Point location, ObjectContent content, ObjectType type) : this(plataformSize, location, content, type, Properties.Resources._default)
        {

        }
        public GraphicObject(Size plataformSize, Point location, ObjectContent content, ObjectType type, Image viewContent) : base(plataformSize, location, content, type)
        {
            ViewContent = viewContent;
            isVisible = true;
            Drawing += new DrawingHandler((object sender, DrawingEventArgs args) => { 
            
            });
            FinishDrawing += new FinishedDrawingHandler((object sender, FinishedDrawingArgs args)=> {
                return args.OutPut;
            });
        }

        public event DrawingHandler Drawing;
        public event FinishedDrawingHandler FinishDrawing;
        public DrawResult Draw(Size uiSize)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Drawing.Invoke(this, new DrawingEventArgs());

            DrawResult drawResult = new DrawResult(new Bitmap(ViewContent,
                new System.Drawing.Size(uiSize.Width/PlataformSize.Width,uiSize.Height/PlataformSize.Height)), 
                    Location);

            stopwatch.Stop();

            FinishDrawing.Invoke(this, new FinishedDrawingArgs(drawResult.Image, stopwatch.Elapsed));
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

        public bool Equals(IGraphicObject other)
        {
            if (other.ID.Equals(this.ID))
            {
                return true;
            }
            return base.Equals(other);
        }
    }

}