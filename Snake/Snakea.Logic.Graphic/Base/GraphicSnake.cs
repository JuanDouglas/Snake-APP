 using Snake.Logic.Enums;
using System.Drawing;
using Point = Snake.Logic.Base.Point;
namespace Snake.Logic.Graphic.Base
{
    public class GraphicSnake : Snake, IGraphicObject
    {
        public Image ViewContent { get; set; }

        public event DrawingHandler Drawing;
        public event FinishedDrawingHandler FinishDrawing;

        public GraphicSnake(GraphicGamePlataform plataform,Direction direction,Point location) : base(plataform,direction,location) { 
        
        }

        public GraphicSnake(Snake snake) : base(snake.Plataform, snake.Direction, snake.Location)
        {
           
        }

        public Image Draw()
        {
            throw new System.NotImplementedException();
        }

        public Point GetCenterPoint()
        {
            throw new System.NotImplementedException();
        }
    }
}