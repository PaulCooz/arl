using System;
using System.Collections.Generic;
using Common.Interpreters.Expressions;
using UnityEngine;

namespace Common.Interpreters
{
    public static class StandardContext
    {
        public static void Fill
        (
            in IDictionary<string, Core.Function> functions,
            in IDictionary<string, Expression> variables,
            in IDictionary<Core.Token, Core.BinOperation> operations
        )
        {
            functions.Add("min", Min);
            functions.Add("max", Max);
            functions.Add("log", Log);
            functions.Add("error", Error);

            operations.Add(Core.Token.And, And);
            operations.Add(Core.Token.Or, Or);
            operations.Add(Core.Token.Equals, Equals);
            operations.Add(Core.Token.Less, Less);
            operations.Add(Core.Token.Greater, Greater);
            operations.Add(Core.Token.Plus, Plus);
            operations.Add(Core.Token.Minus, Minus);
            operations.Add(Core.Token.Mult, Mult);
            operations.Add(Core.Token.Div, Div);

            variables.Add("pi", new NumberExpression(Mathf.PI));
        }

        private static Expression Min(in IReadOnlyList<Expression> expressions)
        {
            var count = expressions.Count;
            if (count < 1) return new NumberExpression(double.MinValue);

            var num = new NumberExpression(expressions[0].StringValue).Value;
            for (var i = 1; i < count; i++)
            {
                var val = new NumberExpression(expressions[i].StringValue).Value;
                if (val < num)
                {
                    num = val;
                }
            }

            return new NumberExpression(num);
        }

        private static Expression Max(in IReadOnlyList<Expression> expressions)
        {
            var count = expressions.Count;
            if (count < 1) return new NumberExpression(double.MaxValue);

            var num = new NumberExpression(expressions[0].StringValue).Value;
            for (var i = 1; i < count; i++)
            {
                var val = new NumberExpression(expressions[i].StringValue).Value;
                if (val > num)
                {
                    num = val;
                }
            }

            return new NumberExpression(num);
        }

        private static Expression Log(in IReadOnlyList<Expression> expressions)
        {
            Debug.Log(Tools.Concat(expressions));
            return Expression.Empty;
        }

        private static Expression Error(in IReadOnlyList<Expression> expressions)
        {
            Debug.LogError(Tools.Concat(expressions));
            throw new Exception("expression error!");
        }

        private static Expression BinaryOpNumbers(Expression l, Expression r, Func<double, double, double> func)
        {
            var left = new NumberExpression(l.StringValue);
            var right = new NumberExpression(r.StringValue);

            return new NumberExpression(func.Invoke(left.Value, right.Value));
        }

        private static BooleanExpression BinaryOpBoolean(in Expression l, in Expression r, in Func<double, double, bool> func)
        {
            var left = new NumberExpression(l.StringValue);
            var right = new NumberExpression(r.StringValue);

            return new BooleanExpression(func.Invoke(left.Value, right.Value));
        }

        private static BooleanExpression And(in Expression l, in Expression r)
        {
            var left = new BooleanExpression(l.StringValue);
            var right = new BooleanExpression(r.StringValue);

            return new BooleanExpression(left.Value && right.Value);
        }

        private static BooleanExpression Or(in Expression l, in Expression r)
        {
            var left = new BooleanExpression(l.StringValue);
            var right = new BooleanExpression(r.StringValue);

            return new BooleanExpression(left.Value || right.Value);
        }

        private static Expression Plus(in Expression l, in Expression r)
        {
            if (l.IsString() || r.IsString())
            {
                return new StringExpression($"'{l.StringValue.ClearString()}{r.StringValue.ClearString()}'");
            }

            return BinaryOpNumbers(l, r, (a, b) => a + b);
        }

        private static Expression Minus(in Expression l, in Expression r)
        {
            return BinaryOpNumbers(l, r, (a, b) => a - b);
        }

        private static Expression Mult(in Expression l, in Expression r)
        {
            return BinaryOpNumbers(l, r, (a, b) => a * b);
        }

        private static Expression Div(in Expression l, in Expression r)
        {
            return BinaryOpNumbers(l, r, (a, b) => a / b);
        }

        private static BooleanExpression Less(in Expression l, in Expression r)
        {
            if (l.IsString() && r.IsString())
            {
                return new BooleanExpression(string.CompareOrdinal(l.StringValue, r.StringValue) < 0);
            }

            return BinaryOpBoolean(l, r, (a, b) => a < b);
        }

        private static BooleanExpression Greater(in Expression l, in Expression r)
        {
            if (l.IsString() && r.IsString())
            {
                return new BooleanExpression(string.CompareOrdinal(l.StringValue, r.StringValue) > 0);
            }

            return BinaryOpBoolean(l, r, (a, b) => a > b);
        }

        private static BooleanExpression Equals(in Expression l, in Expression r)
        {
            if (l.IsString() && r.IsString())
            {
                var strA = l.StringValue.WithoutQuotes();
                var strB = r.StringValue.WithoutQuotes();
                return new BooleanExpression(string.CompareOrdinal(strA, strB) == 0);
            }

            return BinaryOpBoolean(l, r, (a, b) => Math.Abs(a - b) <= double.Epsilon);
        }
    }
}