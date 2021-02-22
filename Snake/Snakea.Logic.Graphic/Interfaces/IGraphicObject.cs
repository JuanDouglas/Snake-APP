using Snake.Logic.Base;
using Snake.Logic.Base.Interfaces;
using Snake.Logic.Graphic.EventArgs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Point = Snake.Logic.Base.Point;

namespace Snake.Logic.Graphic.Base.Interfaces
{
    public interface IGraphicObject : IPlataformObject
    {
        Image ViewContent { get; set; }
        event DrawingHandler Drawing;
        event FinishedDrawingHandler FinishDrawing;
        Image Draw();
        Point GetCenterPoint();
    }
    public delegate void DrawingHandler(object sender, DrawingEventArgs args);
    public delegate Image FinishedDrawingHandler(object sender, FinishedDrawingArgs args);
}
