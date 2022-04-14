using UnityEngine;
using System.Collections.Generic;

namespace UCARPG.Utilities
{
    public class ObjectPool<T> where T : RecyclableObject
    {
        private Queue<T> _clones = new Queue<T>();
        private T _original;
        private Transform _cloneParent;

        public ObjectPool(T original, Transform cloneParent, int initialCount)
        {
            _original = original;
            _cloneParent = cloneParent;
            for (int i = 0; i < initialCount; i++)
            {
                CloneToContainer();
            }
        }

        public T GetObject()
        {
            if (_clones.Count == 0)
            {
                CloneToContainer();
            }
            T result = _clones.Dequeue();
            return result;
        }

        private void CloneToContainer()
        {
            T clone = GameObject.Instantiate(_original, _cloneParent);
            clone.Sleeped += recyclableObject => _clones.Enqueue(recyclableObject as T);
            _clones.Enqueue(clone);
        }
    }
}