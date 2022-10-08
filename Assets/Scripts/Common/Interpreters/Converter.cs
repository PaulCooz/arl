using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Common.Interpreters
{
    public static class Converter
    {
        public static Value ToScriptValue(this double v)
        {
            return new Value(v.ToString(Core.NumberFormat));
        }

        public static Value ToScriptValue(this int v)
        {
            return new Value(v.ToString());
        }

        public static Value ToScriptValue(this string v)
        {
            return new Value($"'{v}'");
        }

        public static Value ToScriptValue(this Vector2 v)
        {
            return new Value($"[{v.x.ToString(Core.NumberFormat)};{v.y.ToString(Core.NumberFormat)}]");
        }

        public static Value ToScriptValue(this IReadOnlyList<double> arr)
        {
            return arr.ToScriptValue(arg => arg.ToString(Core.NumberFormat));
        }

        public static IReadOnlyList<Value> ToValues(this IEnumerable<Expression> expressions)
        {
            return expressions.Select(expression => new Value(expression.StringValue)).ToArray();
        }

        private static Value ToScriptValue<T>(this IReadOnlyList<T> arr, Func<T, string> convert)
        {
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
    }
}