using Snake.Logic.Graphic.Base;
using Snake.Logic.Graphic.Base.Interfaces;
using Snake.Logic.Graphic.EventArgs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Logic.Graphic
{
    public class UI
    {
        public Background Background { get; set; }


        public delegate Image FinishedDrawingHandler();
        public event FinishedDrawingHandler FinishDrawing;
        public GamePlataform Plataform { get; set; }

        public UI(in GamePlataform plataform, int width, int height) {
            Background = new Background(plataform,width,height);
            Plataform = plataform;
        }

        private Image Draw(IGraphicObject[] graphicObjects) {
            throw new NotImplementedException();
        }
    }
}
