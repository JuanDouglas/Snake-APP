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
        public int Height { get => _height; set { _height = value; } }
        private int _height;

        public delegate Image FinishedDrawingHandler();
        public event FinishedDrawingHandler FinishDrawing;
        public GraphicGamePlataform GamePlataform
        {
            get => _gamePlataform; set
            {
                _gamePlataform = value;
                Background = new Background(GamePlataform, Width, Height);
                GamePlataform.UpdateView += new GamePlataform.UpdateViewHandler((object sender, UpdateViewArgs args) =>
                {

                });
                GamePlataform.LoseGame += new GamePlataform.LoseGameHandler((object sender, LoseGameArgs args) =>
                {
                    GamePlataform.Stop();
                });
            }
        }
        private GraphicGamePlataform _gamePlataform;
        public GameUI(in GraphicGamePlataform plataform, int width, int height)
        {
            Width = width;
            Height = height;
            if (plataform != null)
            {
                GamePlataform = plataform;
            }
        }

        public Image Draw()
        {
            Bitmap result = (Bitmap)Background.GetImage();

            foreach (var item in GamePlataform.GraphicObjects)
            {
                DrawResult drawResult = item.Draw(new Size(Width, Height));
                result = DrawImage(result, (Bitmap)drawResult.Image,
                    Background.GetPointByLocation(drawResult.CenterPoint));
            }
            return result;
        }

        private Bitmap DrawImage(Bitmap background, Bitmap image, Point point)
        {
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color colorPixel = image.GetPixel(x, y);
                    Color empty = Color.FromArgb(0, 0, 0, 0);
                    if (colorPixel != empty && colorPixel != Color.Empty && colorPixel != Color.Transparent)
                    {
                        background.SetPixel(x + point.X, y + point.Y, image.GetPixel(x, y));
                    }
                }
            }
            return background;
        }
    }
}
