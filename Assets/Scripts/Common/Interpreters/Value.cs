using System;
using System.Collections.Generic;

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

        public Value(in IReadOnlyList<Expression> expressions)
        {
            switch (expressions.Count)
            {
                case <= 0:
                    StringValue = "0";
                    return;

                case 1:
                    StringValue = expressions[0].StringValue;
                    return;

                default:
                    throw new ArgumentException("can't parse more than 1 expression");
            }
        }
    }
}