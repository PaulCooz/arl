using System;

namespace Common.Interpreters
{
    public readonly struct Value
    {
        public static readonly Value Null = new("0");

        public readonly string StringValue;

        public int IntValue => Convert.ToInt32(StringValue);
        public double DoubleValue => Convert.ToDouble(StringValue);

        public Value(string stringValue)
        {
            StringValue = stringValue;
        }
    }
}