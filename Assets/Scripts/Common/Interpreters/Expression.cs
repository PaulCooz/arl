using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Common.Interpreters
{
    public class Expression
    {
        public string StringValue;

        public Expression(string stringValue)
        {
            StringValue = stringValue;
        }
    }

    public class VariableExpression : Expression
    {
        public readonly string Name;
        public readonly Expression Value;

        public VariableExpression(string name, Expression value) : base(name)
        {
            Name = name;
            Value = value;
        }
    }

    public class NumberExpression : Expression
    {
        public readonly int ValueInt;
        public readonly double ValueDouble;

        public readonly bool IsInt;

        public NumberExpression(string value) : base(value)
        {
            Tools.ParseNumber(value, out ValueDouble, out ValueInt, out IsInt);

            if (!IsInt && value[^1] != 'f') StringValue += 'f';
        }

        public NumberExpression(int value) : base(value.ToString())
        {
            IsInt = true;
            ValueInt = value;
            ValueDouble = value;
        }

        public NumberExpression(double value) : base(value.ToString())
        {
            IsInt = false;
            ValueInt = (int) value;
            ValueDouble = value;
            StringValue += 'f';
        }
    }

    public class BooleanExpression : Expression
    {
        private static readonly Regex BooleanTruePattern = new("^(1|true|t|yes|y|on)$", RegexOptions.IgnoreCase);

        public BooleanExpression(bool value) : base(value.ToString()) { }
        public BooleanExpression(string value) : base(BooleanTruePattern.IsMatch(value).ToString()) { }
    }

    public class BinaryExpression : Expression
    {
        public readonly Expression Expression;

        public BinaryExpression(in Token token, in Expression left, in Expression right)
            : base(token.ToString())
        {
            Expression = GetExpression(token, left, right);
        }

        private Expression GetExpression(in Token token, in Expression left, in Expression right)
        {
            return token switch
            {
                Token.And => Operations.And(left, right),
                Token.Or => Operations.Or(left, right),
                Token.Equals => Operations.Equals(left, right),
                Token.Less => Operations.Less(left, right),
                Token.Greater => Operations.Greater(left, right),

                Token.Plus => Operations.Sum(left, right),
                Token.Minus => Operations.Sub(left, right),
                Token.Mult => Operations.Mul(left, right),
                Token.Div => Operations.Div(left, right),

                _ => throw new ArgumentOutOfRangeException(nameof(token), token, null)
            };
        }
    }

    public class CallExpression : Expression
    {
        public CallExpression(string name, IReadOnlyList<Expression> args)
            : base(CallMethod(name, args)) { }

        private static string CallMethod(string name, IReadOnlyList<Expression> args)
        {
            var values = new object[args.Count];
            for (var i = 0; i < args.Count; i++)
            {
                values[i] = (float) new NumberExpression(args[i].StringValue).ValueDouble;
            }

            var method = typeof(UnityEngine.Mathf)
                .GetMethods()
                .FirstOrDefault(method => Tools.EqualsMethod(values, method, name));

            return method != null ? method.Invoke(null, values).ToString() : throw new NullReferenceException();
        }
    }
}