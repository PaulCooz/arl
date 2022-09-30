using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Common.Interpreters
{
    public static class Tools
    {
        public static bool IsDigitOrDot(in char c)
        {
            return char.IsDigit(c) || c is '.' or ',';
        }

        public static void ParseNumber(in string value, out double valDouble, out int valInt, out bool isInt)
        {
            isInt = true;
            var sb = new StringBuilder();
            foreach (var c in value)
            {
                if (c != 'f')
                {
                    var decSep = c is '.' or ',';
                    if (decSep) isInt = false;

                    sb.Append(decSep ? '.' : c);
                }
                else
                {
                    isInt = false;
                }
            }

            valDouble = double.Parse(sb.ToString(), new NumberFormatInfo {NumberDecimalDigits = '.'});
            valInt = (int) valDouble;
        }

        public static void ParseNumber(in string value, out int valInt)
        {
            ParseNumber(value, out _, out valInt, out _);
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