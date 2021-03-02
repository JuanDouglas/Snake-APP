using Snake.Logic.Base;
using Snake.Logic.Base.Interfaces;
using Snake.Logic.Enums;
using Snake.Logic.EventArgs;
using Snake.Logic.Graphic.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Snake.Logic.Graphic.Base
{
    public class GraphicGamePlataform : GamePlataform
    {
        public override List<IPlataformObject> Objects
        {
            get => objects; set => objects = value;

        }
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
                    GraphicSnake = new GraphicSnake(value);
                }
            }
        }
        public GraphicSnake GraphicSnake { get; set; }
        private List<IPlataformObject> objects;
        private List<IGraphicObject> graphicObjects;
        public IList<IGraphicObject> GraphicObjects { get => graphicObjects; }
        public override event UpdateViewHandler UpdateView;

        public GraphicGamePlataform(GamePlataform plataform) : this(plataform.Size.Width,
            plataform.Size.Height,
            plataform.Velocity,
            plataform.Apples.Count,
            plataform.Snake.Direction,
            plataform.Snake.Location)
        {
            
        }

        public GraphicGamePlataform(int width, int height, int velocity) : this(width, height, velocity, 3, Direction.Right, new Point(0, 0))
        {

        }
        public GraphicGamePlataform(int width, int height, int velocity, int apples, Direction snake_direction, Point snake_start_point) : base(width, height, velocity, apples, snake_direction, snake_start_point)
        {
            UpdateView += new UpdateViewHandler((object sender, UpdateViewArgs args) =>
            {
                _ = sender;
            });
            base.UpdateView += new UpdateViewHandler((object sender, UpdateViewArgs args) =>
            {
                UpdateView.Invoke(sender, args);
            });

        }

        public override void RemoveObject(IPlataformObject @object)
        {
            objects.RemoveAll(rmall=>rmall.ID==@object.ID);

            if (graphicObjects == null)
            {
                graphicObjects = new List<IGraphicObject>();
            }
            graphicObjects.RemoveAll(rmall => rmall.ID == @object.ID);
        }

        public override void AddObject(IPlataformObject @object)
        {
            RemoveObject(@object);
            objects.Add(@object);
           
            if (!(@object is IGraphicObject))
            {
                @object = new GraphicObject(@object);
            }

            IGraphicObject thisObject = @object as IGraphicObject;

            if (graphicObjects==null)
            {
                graphicObjects = new List<IGraphicObject>();
            }

            graphicObjects.RemoveAll(rmall => rmall.ID == @object.ID);
            graphicObjects.Add(thisObject);
        }
      

    }

}
