namespace Common.Interpreters
{
    public readonly struct Value
    {
        public static readonly Value Null = new("0");

        public readonly string StringValue;

        public Value(string stringValue)
        {
            StringValue = stringValue;
        }
    }
}