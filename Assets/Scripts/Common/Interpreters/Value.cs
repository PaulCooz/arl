using System;

namespace Common.Interpreters
{
    public readonly struct Value
    {
        public static readonly Value Null = new("0");

        public readonly string StringValue;

        public int IntValue
        {
            get
            {
                Tools.ParseNumber(StringValue, out var result);
                return result;
            }
        }

        public double DoubleValue => Convert.ToDouble(StringValue);

        public Value(string stringValue)
        {
            StringValue = stringValue;
        }
    }
}