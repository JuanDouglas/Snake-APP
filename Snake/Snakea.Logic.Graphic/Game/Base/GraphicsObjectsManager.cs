using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Logic.Graphic.Game.Base
{
    public class GraphicsObjectsManager 
    {
        private List<GraphicObject> plataformObjects;
        public GraphicObject this[int index] { get => plataformObjects[index]; set => plataformObjects[index] = value; }
        public GraphicObject this[Guid id] { get => plataformObjects.FirstOrDefault(fs => fs.ID.Equals(id)); set => plataformObjects[PositionByID(id)] = value; }


        public GraphicsObjectsManager()
        {
            plataformObjects = new List<GraphicObject>();
        }

        public int Count => plataformObjects.Count;

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(GraphicObject item)
        {
            plataformObjects.Add(item);
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(GraphicObject item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(GraphicObject[] array, int arrayIndex)
        {
            plataformObjects.CopyTo(array, arrayIndex);
        }

        private int PositionByID(Guid id)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].ID.Equals(id))
                {
                    return i - 1;
                }
            }
            return 0;
        }
        public int IndexOf(GraphicObject item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, GraphicObject item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(GraphicObject item)
        {
            if (plataformObjects.RemoveAll(re => re.ID == item.ID) >= 1)
            {
                return true;
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }


        public IEnumerator<GraphicObject> GetEnumerator()
        {
            return plataformObjects.GetEnumerator();
        }
    }

}
