using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
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

        public static bool EqualsMethod(in IReadOnlyList<object> args, in MethodInfo method, in string name)
        {
            if (!string.Equals(method.Name, name, StringComparison.OrdinalIgnoreCase)) return false;

            var parameters = method.GetParameters();
            var size = Math.Min(parameters.Length, args.Count);
            var equalsTypes = false;

            for (var i = 0; i < size; i++)
            {
                if (parameters[i].ParameterType != args[i].GetType()) continue;

                equalsTypes = true;
                break;
            }

            return equalsTypes;
        }
    }
}