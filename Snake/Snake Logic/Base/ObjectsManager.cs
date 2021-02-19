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
        public PlataformObject this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(PlataformObject item)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        IEnumerator<PlataformObject> IEnumerable<PlataformObject>.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
