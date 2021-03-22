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

        public GameUI(int width, int height) :
            this(new GraphicGamePlataform(width, height, Logic.GamePlataform.DefaultVelocity), width, height)
        {

        }
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
            for (int i = 0; i < GamePlataform.GraphicObjects.Count; i++)
            {
                DrawResult drawResult = GamePlataform.GraphicObjects[i].Draw(new Size(Width, Height));
                result = DrawImage(result, new Bitmap(drawResult.Image),
                    Background.GetPointByLocation(drawResult.CenterPoint));
            }
            return result;
        }

        private Bitmap DrawImage(Bitmap background, Bitmap image, Point point)
        {
            Bitmap local = new Bitmap(image);
            for (int x = 0; x < local.Width; x++)
            {
                for (int y = 0; y < local.Height; y++)
                {

                    Color colorPixel = local.GetPixel(x, y);
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
