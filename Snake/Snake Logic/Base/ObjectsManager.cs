using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Logic.Base
{
    public class ObjectsManager :  IList<PlataformObject>
    {
        private List<PlataformObject> plataformObjects;
        public PlataformObject this[int index] { get => plataformObjects[index]; set => plataformObjects[index] = value; }
        public PlataformObject this[Guid id] { get => plataformObjects.FirstOrDefault(fs=>fs.ID.Equals(id)); set => plataformObjects[PositionByID(id)] = value; }


        public ObjectsManager() {
            plataformObjects = new List<PlataformObject>();
        }

        public int Count => plataformObjects.Count;

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(PlataformObject item)
        {
            plataformObjects.Add(item);
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(PlataformObject item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(PlataformObject[] array, int arrayIndex)
        {
            plataformObjects.CopyTo(array,arrayIndex);
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        private int PositionByID(Guid id) {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].ID.Equals(id))
                {
                    return i-1;
                }
            }
            return 0;
        }
        public int IndexOf(PlataformObject item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, PlataformObject item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(PlataformObject item)
        {
            if (plataformObjects.RemoveAll(re => re.ID == item.ID)>=1)
            {
                return true;
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        IEnumerator<PlataformObject> IEnumerable<PlataformObject>.GetEnumerator()
        {
            return plataformObjects.GetEnumerator();
        }
    }
}
