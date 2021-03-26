using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace Snake.Logic.Graphic.Base
{
    /// <summary>
    /// Background da plataforma.
    /// </summary>
    public class Background
    {
        /// <summary>
        /// Plataforma de jogo que será desenhada.
        /// </summary>
        public GraphicGamePlataform Plataform { get; set; }
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
        /// <summary>
        /// Cor primária do fundo.
        /// </summary>
        public Color ColorPrimaryDark { get; set; }
        /// <summary>
        /// Cor secundário do fundo.
        /// </summary>
        public Color ColorPrimaryLight { get; set; }
        /// <summary>
        /// Quantidade de blocos no desenho.
        /// </summary>
        public List<Block> Blocks { get; private set; }
        public Image BackgroundPather { get => GetImage(); }
        public Background(in GraphicGamePlataform plataform, int width, int height)
        {
            Plataform = plataform;
            Width = width;
            Height = height;
            ColorPrimaryDark = Color.FromArgb(255, 142, 204, 57);
            ColorPrimaryLight = Color.FromArgb(255, 168, 217, 73);
            BlockWidth = Width / Plataform.Size.Width;
            BlockHeight = Height / Plataform.Size.Height;
            Blocks = new List<Block>();
            for (int x = 0; x < Plataform.Size.Width; x++)
            {
                for (int y = 0; y < Plataform.Size.Height; y++)
                {
                    Blocks.Add(new Block(new Point((Width / Plataform.Size.Width) * x, (Height / Plataform.Size.Height) * y), new Point(x, y)));
                }
            }
        }
        public Point GetPointByLocation(Logic.Base.Point point)
        {
            return Blocks.FirstOrDefault(fs => fs.PlataformAxis.X == point.X && fs.PlataformAxis.Y == point.Y).Axis;
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
