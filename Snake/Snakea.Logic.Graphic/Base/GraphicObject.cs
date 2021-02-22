using Snake.Logic.Base;
using Snake.Logic.Base.Interfaces;
using Snake.Logic.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Point = Snake.Logic.Base.Point;

namespace Snake.Logic.Graphic.Base
{

    public class GraphicGamePlataform : GamePlataform
    {
        public override List<IPlataformObject> Objects { get => GraphicObjects as List<IPlataformObject>; }
        public override Snake Snake
        {
            get => GraphicSnake as Snake; set
            {
                if (value is GraphicSnake)
                {
                    GraphicSnake = value as GraphicSnake;
                }
                else
                {
                    GraphicSnake = new GraphicSnake(Snake);
                }
            } 
        }
        public GraphicSnake GraphicSnake { get; set; }

        private IList<IGraphicObject> GraphicObjects;
        public GraphicGamePlataform(int width, int height, int velocity) : base(width, height, velocity)
        {

        }

    }

    public class GraphicObject : PlataformObject, IGraphicObject
    {
        public Image ViewContent { get; set; }
        public GraphicObject(Point location, ObjectContent content, ObjectType type, Image viewContent) : base(location, content, type)
        {

        }

        public event DrawingHandler Drawing;
        public event FinishedDrawingHandler FinishDrawing;
        public Image Draw()
        {
            return ViewContent;
        }

        public Point GetCenterPoint()
        {
            return this.Location;
        }
    }
   
}
