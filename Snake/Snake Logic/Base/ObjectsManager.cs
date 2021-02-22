using Snake.Logic.Base.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Logic.Base
{
    public abstract class ObjectsManager:  IList<IPlataformObject>
    {
        private List<IPlataformObject> plataformObjects;
        public IPlataformObject this[int index] { get => plataformObjects[index]; set => plataformObjects[index] = value; }
        public IPlataformObject this[Guid id] { get => plataformObjects.FirstOrDefault(fs=>fs.ID.Equals(id)); set => plataformObjects[PositionByID(id)] = value; }


        public ObjectsManager() {
            plataformObjects = new List<IPlataformObject>();
        }

        public virtual int Count => plataformObjects.Count;

        public virtual bool IsReadOnly => throw new NotImplementedException();

        public virtual void Add(IPlataformObject item)
        {
            plataformObjects.Add(item);
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(IPlataformObject item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(IPlataformObject[] array, int arrayIndex)
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
        public int IndexOf(IPlataformObject item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, IPlataformObject item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(IPlataformObject item)
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

        IEnumerator<IPlataformObject> IEnumerable<IPlataformObject>.GetEnumerator()
        {
            return plataformObjects.GetEnumerator();
        }
    }
}
