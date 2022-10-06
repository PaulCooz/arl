using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Interpreters
{
    public static class Tools
    {
        internal static bool IsNumberPart(in char c)
        {
            return char.IsDigit(c) || c is '.' or ',' or 'e' or 'E';
        }

        internal static bool IsQuote(in char c)
        {
            return c is '\"' or '\'';
        }

        internal static void ParseNumber(in string str, out double res)
        {
            var sb = new StringBuilder();
            foreach (var c in str)
            {
                sb.Append(c == ',' ? '.' : c);
            }

            res = double.Parse(sb.ToString(), Core.NumberFormat);
        }

        internal static string ToStrArray(this IEnumerable<Expression> elements)
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

        internal static IReadOnlyList<Value> ToValues(this string array)
        {
            var expressions = array.Substring(1, array.Length - 2).Split(';');

            return expressions.Select(expression => new Value(expression)).ToArray();
        }

        internal static string Concat(IEnumerable<Expression> expressions)
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
    }
}