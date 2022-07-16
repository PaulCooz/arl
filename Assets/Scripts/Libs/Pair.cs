using System;
using UnityEngine;

namespace Libs
{
    [Serializable]
    public class Pair<T0, T1>
    {
        [SerializeField]
        protected T0 left;
        [SerializeField]
        protected T1 right;

        public Pair(T0 left, T1 right)
        {
            this.left = left;
            this.right = right;
        }
    }
}
