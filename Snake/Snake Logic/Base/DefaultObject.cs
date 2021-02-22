using Snake.Logic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Logic.Base
{
    public class DefaultObject : PlataformObject
    {
        public DefaultObject(Size plataformSize,Point location,ObjectContent content,ObjectType type) : base(plataformSize,location, content,type) { 
        
        }
    }
}
