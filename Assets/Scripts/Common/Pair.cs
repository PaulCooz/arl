namespace Common
{
    public abstract class Pair<T0, T1>
    {
        public T0 first;
        public T1 second;

        protected Pair(in T0 first, in T1 second)
        {
            this.first = first;
            this.second = second;
        }
    }
}