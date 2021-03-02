using Snake.Logic.Enums;
using Snake.Logic.Graphic.Base.Interfaces;
using System.Drawing;
using Point = Snake.Logic.Base.Point;

namespace Snake.Logic.Graphic.Base
{
    public class GraphicSnake : Snake, IGraphicObject
    {
        public Image ViewContent { get;  }
        public bool isVisible { get; set; }
        public int UpdateVersion { get; set; }

        public GraphicSnake(in GraphicGamePlataform plataform, in Direction direction, in Point location) : base(plataform,direction,location) { 
        
        }

        public GraphicSnake(in Snake snake) : base(snake.Plataform, snake.Direction, snake.Location)
        {
           
        }

        public event DrawingHandler Drawing;
        public event FinishedDrawingHandler FinishDrawing;

        public DrawResult Draw(in Logic.Base.Size uiSize)
        {
            throw new System.NotImplementedException();
        }

        public DrawResult Draw(in Logic.Base.Size uiSize, in Logic.Base.Size maxSize)
        {
            throw new System.NotImplementedException();
        }

        public bool Equals(IGraphicObject other)
        {
            if (other.ID.Equals(this.ID))
            {
                return true;
            }
            return base.Equals(other);
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}