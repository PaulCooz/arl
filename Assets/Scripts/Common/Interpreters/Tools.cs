using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Common.Interpreters
{
    public static class Tools
    {
        public static bool IsNumberPart(in char c)
        {
            return char.IsDigit(c) || c is '.' or ',' or 'e' or 'E';
        }

        public static void ParseNumber(in string str, out double res)
        {
            var sb = new StringBuilder();
            foreach (var c in str)
            {
                var decSep = c is '.' or ',';
                sb.Append(decSep ? '.' : c);
            }

            res = double.Parse(sb.ToString(), new NumberFormatInfo {NumberDecimalDigits = '.'});
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
    }
}