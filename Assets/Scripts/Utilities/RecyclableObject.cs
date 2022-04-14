using System;
using UnityEngine;

namespace UCARPG.Utilities
{
    public abstract class RecyclableObject : MonoBehaviour
    {
        public event Action<RecyclableObject> Sleeped;

        protected virtual void Sleep()
        {
            Sleeped?.Invoke(this);
        }
    }
}