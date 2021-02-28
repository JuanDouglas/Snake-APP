using Snake.Logic.Base;
using Snake.Logic.Graphic.Game.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Logic.Graphic.Game
{
    public class GraphicPlataform : GamePlataform
    {
        public GraphicsObjectsManager GraphicObjects { get {
                GraphicsObjectsManager objects = new GraphicsObjectsManager();
                foreach (var item in Objects.ToArray())
                {
                    objects.Add((GraphicObject)((PlataformObject)item));
                }
                return objects;
            }
        }
        public GraphicPlataform(int width, int height, int velocity) : base(width, height, velocity) {
        

        }
    }
}
