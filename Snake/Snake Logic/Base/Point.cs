namespace Snake.Logic.Base
{
    /// <summary>
    /// Ponto em plano 2D.
    /// </summary>
    public struct Point
    {
        /// <summary>
        /// Construtor do ponto
        /// </summary>
        /// <param name="x">Local na direção X.</param>
        /// <param name="y">Local na direção Y.</param>
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        /// <summary>
        /// Ponto na direção X
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Ponto na direção Y.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Método respoonsável por verificar se um objeto e igual (Equal) a este objeto.
        /// </summary>
        /// <param name="obj">Objeto a ser verificado.</param>
        /// <returns>TRUE = Igual, FALSE = Diferente</returns>
        public override bool Equals(object obj)
        {
            if (obj is Point)
            {
                Point point = (Point)obj;
                if (point.X == X)
                {
                    if (point.Y == Y)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    public struct Size {
        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Width { get; set; }
        public int Height { get; set; }

        // override object.Equals
        public bool Equals(Size size)
        {
            if (size.Width==Width&&size.Height==Height)
            {
                return true;
            }
            return base.Equals(size);    
        }
    }
}
