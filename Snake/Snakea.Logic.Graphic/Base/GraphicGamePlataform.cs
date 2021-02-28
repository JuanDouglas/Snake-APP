using Snake.Logic.Base.Interfaces;
using Snake.Logic.Graphic.Base.Interfaces;
using System.Collections.Generic;

namespace Snake.Logic.Graphic.Base
{
    public class GraphicGamePlataform : Logic.GraphicGamePlataform
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

        public delegate void UpdateViewHandler(object sender, Logic.GraphicGamePlataform plataform);
        public event UpdateViewHandler UpdateView;
        public GraphicGamePlataform(int width, int height, int velocity) : base(width, height, velocity)
        {

        }

        public GraphicGamePlataform(Logic.GraphicGamePlataform plataform) : base(plataform.Size.Width, 
            plataform.Size.Height,
            plataform.Velocity,
            plataform.Apples.Count,
            plataform.Snake.Direction,
            plataform.Snake.Location)
        {

        }
    }
   
}
