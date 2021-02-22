using Snake.Logic.Base.Interfaces;
using Snake.Logic.Graphic.Base.Interfaces;
using System.Collections.Generic;

namespace Snake.Logic.Graphic.Base
{
    public class GraphicGamePlataform : GamePlataform
    {
        public override List<IPlataformObject> Objects { get => GraphicObjects as List<IPlataformObject>; }
        public override Snake Snake
        {
            get => GraphicSnake; set
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
   
}
