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
using Size = Snake.Logic.Base.Size;

namespace Snake.Logic.Graphic.Base.Interfaces
{
    public interface IGraphicObject : IPlataformObject, IEquatable<IGraphicObject>, IDisposable
    {
        bool isVisible { get; set; }
        int UpdateVersion { get; set; }
        event DrawingHandler Drawing;
        event FinishedDrawingHandler FinishDrawing;
        DrawResult Draw(in Size uiSize);
        DrawResult Draw(in Size uiSize,in Size maxSize);
    }
    public delegate void DrawingHandler(object sender, DrawingEventArgs args);
    public delegate Image FinishedDrawingHandler(object sender, FinishedDrawingArgs args);
}
