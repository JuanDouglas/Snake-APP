using System.Drawing;

namespace Snake.Logic.Graphic.EventArgs
{
    public class FinishedDrawingArgs
    {
        public Image OutPut { get; set; }
        public UI UI { get; set; }
    }
}