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
        public Plataform Plataform { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int BlockWidth { get; set; }
        public int BlockHeight { get; set; }
        public Color ColorPrimaryDark { get; set; }
        public Color ColorPrimaryLight { get; set; }
        public Background(in Plataform plataform, int width, int height)
        {
            Plataform = plataform;
            Width = width;
            Height = height;
            ColorPrimaryDark = Color.FromArgb(142, 204, 57);
            ColorPrimaryLight = Color.FromArgb(167, 0, 0);
            BlockWidth = Width / Plataform.Width;
            BlockHeight = Height / Plataform.Height;
        }
        public Image GetImage()
        {
            Bitmap bitmapImage = new Bitmap(Width, Height);

            bool positive = false;
            bool verticalPositive = false;
            int verticalCount = 0;
            int horizontalCount = 0;
            for (int y = 0; y < Height; y++)
            {

                for (int x = 0; x < Width; x++)
                {
                    if (positive)
                    {
                        bitmapImage.SetPixel(x, y, ColorPrimaryLight);
                    }

                    if (!positive)
                    {
                        bitmapImage.SetPixel(x, y, ColorPrimaryDark);
                    }

                    if (horizontalCount == BlockWidth)
                    {
                        if (positive)
                        {
                            positive = false;
                        }
                        else
                        {
                            positive = true;
                        }
                        horizontalCount = 0;
                    }
                    horizontalCount++;
                }



                if (verticalCount == BlockHeight)
                {
                    if (verticalPositive)
                    {
                        verticalPositive = false; 
                        positive = verticalPositive;
                    }
                    else
                    {
                        verticalPositive = true;
                        positive = verticalPositive;
                    }
                    verticalCount = 0;
                }
                verticalCount++;
            }




            return bitmapImage;
        }
    }
}
