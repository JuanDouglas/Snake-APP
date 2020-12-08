using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Snake.Logic;
namespace Snake.Logic.Graphic
{
    public class Background
    {
        private Plataform Plataform;
        public int BlockWidth { get; set; }
        public int BlockHeight { get; set; }
        public Color ColorPrimaryDark { get; set; }
        public Color ColorPrimaryLight { get; set; }
        public Background(Plataform plataform)
        {
            Plataform = plataform;
            ColorPrimaryDark = Color.FromArgb(142, 204, 57);
            ColorPrimaryLight = Color.FromArgb(167, 0, 0);
            BlockWidth = 8;
            BlockHeight = 8;
        }
        public Image GetImage()
        {
            Bitmap bitmapImage = new Bitmap(Plataform.Width * BlockWidth, Plataform.Height * BlockHeight);
            bool postive = false;
            for (int w = 0; w <  Plataform.Width; w++)
            {
                if (postive) { postive = false; }
                else { postive = true; }
                for (int i = 0; i < BlockWidth; i++)
                {
                    for (int h = 0; h < Plataform.Height; h++)
                    {
                        if (postive) { postive = false; }
                        else { postive = true; }
                        for (int x = 0; x < BlockHeight; x++)
                        {
                            if (postive) { bitmapImage.SetPixel((w * i), (h * x), ColorPrimaryLight); }
                            else { bitmapImage.SetPixel((w * i), (h * x), ColorPrimaryDark); }
                        }
                    }
                }
            }

            return bitmapImage;
        }
    }
}
