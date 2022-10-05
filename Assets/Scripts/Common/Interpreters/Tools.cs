using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Interpreters
{
    public static class Tools
    {
        public static bool IsNumberPart(in char c)
        {
            return char.IsDigit(c) || c is '.' or ',' or 'e' or 'E';
        }

        public static bool IsQuote(in char c)
        {
            return c is '\"' or '\'';
        }

        public static void ParseNumber(in string str, out double res)
        {
            var sb = new StringBuilder();
            foreach (var c in str)
            {
                sb.Append(c == ',' ? '.' : c);
            }

            res = double.Parse(sb.ToString(), Core.NumberFormat);
        }

        public static string ToArray(IEnumerable<Expression> elements)
        {
            var sb = new StringBuilder();
            var isFirst = true;

            sb.Append('[');
            foreach (var element in elements)
            {
                if (isFirst)
                {
                    isFirst = false;
                    sb.Append(element.StringValue);
                }
                else
                {
                    sb.Append(';');
                    sb.Append(element.StringValue);
                }
            }

            sb.Append(']');
            return sb.ToString();
        }

        public static string Concat(IEnumerable<Expression> expressions)
        {
            var sb = new StringBuilder();
            foreach (var expression in expressions)
            {
                sb.Append
                (
                    expression is StringExpression
                        ? expression.StringValue.Substring(1, expression.StringValue.Length - 2)
                        : expression.StringValue
                );
            }

            return sb.ToString();
        }

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
    }
}