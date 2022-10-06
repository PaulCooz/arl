using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Interpreters
{
    public static class Converter
    {
        public static Value ToScriptValue<T>(this T v, Func<T, string> convert = null)
        {
            if (convert == null) convert = arg => arg.ToString();

            return new Value(convert.Invoke(v));
        }

        public static Value ToScriptValue(this double v)
        {
            return new Value(v.ToString(Core.NumberFormat));
        }

        private static Value ToScriptValue<T>(this IReadOnlyList<T> arr, Func<T, string> convert = null)
        {
            if (convert == null) convert = arg => arg.ToString();

            var sb = new StringBuilder();
            sb.Append('[');
            sb.Append(convert.Invoke(arr[0]));
            for (var i = 1; i < arr.Count; i++)
            {
                sb.Append(';');
                sb.Append(convert.Invoke(arr[i]));
            }

            sb.Append(']');

            return new Value(arr.ToString());
        }

        public static Value ToScriptValue(this IReadOnlyList<double> arr)
        {
            return arr.ToScriptValue(arg => arg.ToString(Core.NumberFormat));
        }

        public static IReadOnlyList<Value> ToValues(this IEnumerable<Expression> expressions)
        {
            return expressions.Select(expression => new Value(expression.StringValue)).ToArray();
        }
    }
}