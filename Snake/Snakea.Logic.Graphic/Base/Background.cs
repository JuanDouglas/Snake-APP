using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Snake.Logic;
using Snake.Logic.Graphic;

namespace Snake.Logic.Graphic
{
    /// <summary>
    /// Background da plataforma.
    /// </summary>
    public class Background
    {
        /// <summary>
        /// Plataforma de jogo que será desenhada.
        /// </summary>
        public GamePlataform Plataform { get; set; }
        /// <summary>
        /// Tamanho largura da plataforam de jogo.
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Tamanho da altura da plataforam de jogo.
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// Tamanho da largura dos blocos em Pixeis.
        /// </summary>
        public int BlockWidth { get; set; }
        /// <summary>
        /// Tamanho da altura dos blocos em Pixeis.
        /// </summary>
        public int BlockHeight { get; set; }
        public Color ColorPrimaryDark { get; set; }
        public Color ColorPrimaryLight { get; set; }
        public List<Block> Blocks { get; private set; }

        public Background(in GamePlataform plataform, int width, int height)
        {
            Plataform = plataform;
            Width = width;
            Height = height;
            ColorPrimaryDark = Color.FromArgb(255, 142, 204, 57);
            ColorPrimaryLight = Color.FromArgb(255, 168, 217, 73);
            BlockWidth = Width / Plataform.Width;
            BlockHeight = Height / Plataform.Height;
            Blocks = new List<Block>();
        }
        public Image GetImage()
        {
            Bitmap bitmapImage = new Bitmap(Width, Height);

            bool positive = false;
            int verticalCount = 0;
            int horizontalCount = 0;
            int axisX = 0;
            int axisY = 0;
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
                        Blocks.Add(
                            new Block(
                                new Point(x - (BlockWidth / 2), y + (BlockHeight / 2)),
                                new Point(axisX, axisY)
                                )
                            );
                        if (positive)
                        {
                            positive = false;
                        }
                        else
                        {
                            positive = true;
                        }
                        horizontalCount = 0;
                        axisX++;
                    }
                    horizontalCount++;
                }

                if (verticalCount == BlockHeight)
                {
                    if (positive)
                    {
                        positive = false;
                    }
                    else
                    {
                        positive = true;
                    }
                    verticalCount = 0;
                    axisX = 0;
                    axisY++;
                }
                verticalCount++;
            }

            return bitmapImage;
        }
    }
}
