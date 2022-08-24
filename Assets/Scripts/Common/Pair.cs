namespace Common
{
    public abstract class Pair<T0, T1>
    {
        public T0 First;
        public T1 Second;

        protected Pair(in T0 first, in T1 second)
        {
            this.First = first;
            this.Second = second;
        }
    }
}