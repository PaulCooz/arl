using System;
using System.Collections.Generic;
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
            functions.Add("if", If);
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

        private static Expression If(in IReadOnlyList<Expression> expressions)
        {
            var condition = new BooleanExpression(expressions[0].StringValue);
            if (condition.Value)
            {
                return expressions[1];
            }
            else
            {
                return expressions.Count > 2 ? expressions[2] : new Expression("");
            }
        }

        private static Expression Min(in IReadOnlyList<Expression> expressions)
        {
            var left = new NumberExpression(expressions[0].StringValue);
            var right = new NumberExpression(expressions[1].StringValue);

            if (left.IsInt && right.IsInt)
            {
                return left.ValueInt > right.ValueInt ? right : left;
            }

            return left.ValueDouble > right.ValueDouble ? right : left;
        }

        private static Expression Max(in IReadOnlyList<Expression> expressions)
        {
            var left = new NumberExpression(expressions[0].StringValue);
            var right = new NumberExpression(expressions[1].StringValue);

            if (left.IsInt && right.IsInt)
            {
                return left.ValueInt < right.ValueInt ? right : left;
            }

            return left.ValueDouble < right.ValueDouble ? right : left;
        }

        private static Expression Log(in IReadOnlyList<Expression> expressions)
        {
            Debug.Log(Tools.ToArray(expressions));
            return new Expression("");
        }

        private static Expression Error(in IReadOnlyList<Expression> expressions)
        {
            Debug.LogError(Tools.ToArray(expressions));
            throw new Exception("expression error!");
        }

        private static Expression BinaryOpNumbers
        (
            Expression l, Expression r,
            Func<int, int, int> opInt, Func<double, double, double> opDbl
        )
        {
            var left = new NumberExpression(l.StringValue);
            var right = new NumberExpression(r.StringValue);

            var result = left.IsInt && right.IsInt
                ? new NumberExpression(opInt.Invoke(left.ValueInt, right.ValueInt))
                : new NumberExpression(opDbl.Invoke(left.ValueDouble, right.ValueDouble));

            return result;
        }

        private static Expression Plus(in Expression l, in Expression r)
        {
            return BinaryOpNumbers(l, r, (a, b) => a + b, (a, b) => a + b);
        }

        private static Expression Minus(in Expression l, in Expression r)
        {
            return BinaryOpNumbers(l, r, (a, b) => a - b, (a, b) => a - b);
        }

        private static Expression Mult(in Expression l, in Expression r)
        {
            return BinaryOpNumbers(l, r, (a, b) => a * b, (a, b) => a * b);
        }

        private static Expression Div(in Expression l, in Expression r)
        {
            return BinaryOpNumbers(l, r, (a, b) => a / b, (a, b) => a / b);
        }

        private static BooleanExpression BinaryOpBoolean
        (
            in Expression l, in Expression r,
            in Func<int, int, bool> opInt, in Func<double, double, bool> opDbl
        )
        {
            var left = new NumberExpression(l.StringValue);
            var right = new NumberExpression(r.StringValue);

            var result = left.IsInt && right.IsInt
                ? new BooleanExpression(opInt.Invoke(left.ValueInt, right.ValueInt))
                : new BooleanExpression(opDbl.Invoke(left.ValueDouble, right.ValueDouble));

            return result;
        }

        private static BooleanExpression And(in Expression l, in Expression r)
        {
            return new BooleanExpression(l.StringValue == r.StringValue);
        }

        private static BooleanExpression Or(in Expression l, in Expression r)
        {
            return new BooleanExpression
            (
                string.Equals(l.StringValue, "true", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(r.StringValue, "true", StringComparison.OrdinalIgnoreCase)
            );
        }

        private static BooleanExpression Less(in Expression l, in Expression r)
        {
            return BinaryOpBoolean(l, r, (a, b) => a < b, (a, b) => a < b);
        }

        private static BooleanExpression Greater(in Expression l, in Expression r)
        {
            return BinaryOpBoolean(l, r, (a, b) => a > b, (a, b) => a > b);
        }

        private static BooleanExpression Equals(in Expression l, in Expression r)
        {
            return BinaryOpBoolean(l, r, (a, b) => a == b, (a, b) => Math.Abs(a - b) <= double.Epsilon);
        }
    }
}