using Snake.Logic.EventArgs;
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
        public int Height { get => _height; set { _height= value; } }
        private int _height;

        public delegate Image FinishedDrawingHandler();
        public event FinishedDrawingHandler FinishDrawing;
        public GraphicGamePlataform Plataform { get; set; }
        public delegate void NewViewCreateHandler(object sender, NewViewCreateEventArgs args);
        public event NewViewCreateHandler NewViewCreate;
        public GameUI(in GraphicGamePlataform plataform, int width, int height)
        {
            Width = width;
            Height = height;
            Plataform = plataform;
            Background = new Background(Plataform, width, height);
            Plataform.UpdateView += new GamePlataform.UpdateViewHandler((object sender, UpdateViewArgs args) =>
            {
                NewViewCreate.Invoke(sender, new NewViewCreateEventArgs(Draw(Plataform.GraphicObjects.ToArray()), Plataform, this));
            });
          
        }

        private Image Draw(IGraphicObject[] graphicObjects)
        {
            Image background = Background.GetImage();
            background.Save($"{Environment.CurrentDirectory}\\Background.jpeg");
            Graphics graphics = Graphics.FromImage(background);
            foreach (var item in graphicObjects)
            {
                if (item.isVisible)
                {
                    DrawResult drawResult = item.Draw(new Size(Width, Height));
                    graphics.DrawImage(drawResult.Image,
                        Background.GetPointByLocation(drawResult.CenterPoint)
                       );
                    graphics.Save();
                    Console.WriteLine($"Draw object ({item.ID}) Elapsed: {drawResult.Elapsed}");
                }
            }

            return background;
        }
    }
}
