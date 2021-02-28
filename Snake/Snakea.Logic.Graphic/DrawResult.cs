using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Point = Snake.Logic.Base.Point;

namespace Snake.Logic.Graphic
{
    /// <summary>
    /// Drawing result for draw graphic object
    /// </summary>
   public class DrawResult
    {
        /// <summary>
        /// Image result for drawing.
        /// </summary>
        public Image Image { get; set; }
        /// <summary>
        /// Point of screen for draw object. 
        /// </summary>
        public Point CenterPoint { get; set; }
        /// <summary>
        /// Drawing time.
        /// </summary>
        public TimeSpan Elapsed { get; set; }
        public DrawResult(Image image, Point centerPoint)
        {
            Image = image ?? throw new ArgumentNullException(nameof(image));
            CenterPoint = centerPoint;
        }
    }
}
