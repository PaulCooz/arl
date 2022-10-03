﻿using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Common.Interpreters
{
    public class Expression
    {
        public string StringValue;

        public static Expression Empty => new("");

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
        public readonly double Value;

        public NumberExpression(string value) : base(value)
        {
            Tools.ParseNumber(value, out Value);
        }

        public NumberExpression(double value) : base(value.ToString())
        {
            Value = value;
        }
    }

    public class BooleanExpression : Expression
    {
        private static readonly Regex BooleanTruePattern = new("^(true|yes|on)$", RegexOptions.IgnoreCase);

        public bool Value => BooleanTruePattern.IsMatch(StringValue);

        public BooleanExpression(bool value) : base(value.ToString()) { }
        public BooleanExpression(string value) : base(BooleanTruePattern.IsMatch(value).ToString()) { }
    }

    public class StringExpression : Expression
    {
        public StringExpression(string value) : base(value) { }
    }

    public class ArrayExpression : Expression
    {
        public ArrayExpression(IEnumerable<Expression> elements) : base(Tools.ToArray(elements)) { }
    }

    public class BinaryExpression : Expression
    {
        public BinaryExpression(in Core.Token token, in Expression left, in Expression right, in Context context)
            : base(GetExpression(token, left, right, context).StringValue) { }

        private static Expression GetExpression
        (
            in Core.Token token,
            in Expression left, in Expression right,
            in Context context
        )
        {
            return context.GetOperation(token).Invoke(left, right);
        }
    }

    public class CallExpression : Expression
    {
        public CallExpression(string name, IReadOnlyList<Expression> args, in Context context)
            : base(CallMethod(name, args, context)) { }

        private static string CallMethod(in string name, in IReadOnlyList<Expression> args, in Context context)
        {
            return context.GetFunction(name).Invoke(args).StringValue;
        }
    }
}