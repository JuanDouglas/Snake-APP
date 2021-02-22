using System;
using System.Drawing;

namespace Snake.Logic.Graphic.EventArgs
{
    public class FinishedDrawingArgs
    {
        public Image OutPut { get; set; }
        public TimeSpan ElapsedTime { get; set; }

        public FinishedDrawingArgs(Image outPut, TimeSpan elapsedTime)
        {
            OutPut = outPut;
            ElapsedTime = elapsedTime;
        }
    }
}