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

        public delegate void DrawUIHandler(object sender, DrawUIArgs args);
        public event DrawUIHandler DrawUI;

        public UI(in Plataform plataform, int width, int height) {
            Background = new Background(plataform,width,height);
            
        }

        private Image Draw(GraphicObject[] graphicObjects) {

            throw new NotImplementedException();
        }
    }
}
