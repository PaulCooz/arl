using System;

namespace Libs
{
    [Serializable]
    public class MinMaxInt : Pair<int, int>
    {
        public int Min => left;
        public int Max => right;

        public MinMaxInt(int min, int max) : base(min, max)
        {
        }
    }
}
