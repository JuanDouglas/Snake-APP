using Snake.Logic.Enums;
using Snake.Logic.Graphic.Base.Interfaces;
using System.Drawing;
using Point = Snake.Logic.Base.Point;

namespace Snake.Logic.Graphic.Base
{
    public class GraphicSnake : Snake, IGraphicObject
    {
        public Image ViewContent { get; set; }
        public bool isVisible { get; set; }
        public int UpdateVersion { get; set; }

        public GraphicSnake(GraphicGamePlataform plataform,Direction direction,Point location) : base(plataform,direction,location) { 
        
        }

        public GraphicSnake(Snake snake) : base(snake.Plataform, snake.Direction, snake.Location)
        {
           
        }

        public event DrawingHandler Drawing;
        public event FinishedDrawingHandler FinishDrawing;

        public DrawResult Draw(Logic.Base.Size uiSize)
        {
            throw new System.NotImplementedException();
        }

        public DrawResult Draw(Logic.Base.Size uiSize, Logic.Base.Size maxSize)
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
    }
}