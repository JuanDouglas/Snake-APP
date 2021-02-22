using Snake.Logic.Graphic.Base;
using Snake.Logic.Graphic.Base.Interfaces;
using Snake.Logic.Graphic.EventArgs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Size = Snake.Logic.Base.Size;
namespace Snake.Logic.Graphic
{
    public class GameUI
    {
        public Background Background { get; set; }
        public int Width { get => _width; set { _width = value; } }
        private int _width;
        public int Height { get => _height; set { _width = value; } }
        private int _height;

        public delegate Image FinishedDrawingHandler();
        public event FinishedDrawingHandler FinishDrawing;
        public GraphicGamePlataform Plataform { get; set; }

        public GameUI(in GraphicGamePlataform plataform, int width, int height)
        {
            Background = new Background(plataform, width, height);
            Plataform = new GraphicGamePlataform(plataform);
        }

        private Image Draw(IGraphicObject[] graphicObjects)
        {
            Graphics graphics = Graphics.FromImage(Background.BackgroundPather);
            foreach (var item in graphicObjects)
            {
                if (item.isVisible)
                {
                   DrawResult drawResult = item.Draw(new Size(Width, Height));
                    graphics.DrawImage(drawResult.Image, 
                        Background.GetPointByLocation(drawResult.CenterPoint)
                       );
                    Console.WriteLine($"Draw object ({item.ID}) Elapsed: {drawResult.Elapsed}");
                }
            }
            return new Bitmap(Width, Height, graphics);
        }
    }
}
