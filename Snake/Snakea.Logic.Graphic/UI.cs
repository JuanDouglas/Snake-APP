using Snake.Logic.Graphic.Base;
using Snake.Logic.Graphic.EventArgs;
using Snake.Logic.Graphic.Game;
using Snake.Logic.Graphic.Game.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Logic.Graphic
{
    public class GameUI
    {
        public Background Background { get; set; }

        public delegate void DrawUIHandler(object sender, DrawUIArgs args);
        public event DrawUIHandler DrawUI;
        public GraphicPlataform Plataform { get; set; }

        public GameUI(in GraphicPlataform plataform, int width, int height)
        {
            Background = new Background(plataform, width, height);
            Plataform = plataform;
        }

        public Image Draw() {
            return Draw((GraphicObject[])Plataform.GraphicObjects.ToArray());
        }
        public Image Draw(GraphicObject[] graphicObjects)
        {
            Bitmap backgroud = (Bitmap)Background.GetImage();
            foreach (var item in Plataform.GraphicObjects)
            {
                Bitmap bitmap = (Bitmap)item.Draw(Background.Width / Plataform.Width, Background.Height / Plataform.Height);
                Graphics graphics = Graphics.FromImage(backgroud);
                graphics.DrawImage(bitmap, Background.BlockByPosition(item.Location).PlataformAxis);
            }
            return backgroud;
        }
    }
}
