using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Logic.Graphic.Game
{
    public class GraphicPlataform : GamePlataform
    {
        public Graphic.Game.Base.GraphicsObjectsManager GraphicObjects { get; set; }
        public GraphicPlataform(int width, int height, int velocity) : base(width, height, velocity) {
        
        }
    }
}
