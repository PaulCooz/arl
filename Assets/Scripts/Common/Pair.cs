using System;

namespace Common
{
    [Serializable]
    public struct Pair<T0, T1>
    {
        public T0 key;
        public T1 value;
    }
}