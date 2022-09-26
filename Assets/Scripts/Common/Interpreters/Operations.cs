using System;

namespace Common.Interpreters
{
    public static class Operations
    {
        private static readonly Func<int, int, int> SumIntFunc = (a, b) => a + b;
        private static readonly Func<double, double, double> SumDblFunc = (a, b) => a + b;

        private static readonly Func<int, int, int> SubIntFunc = (a, b) => a - b;
        private static readonly Func<double, double, double> SubDblFunc = (a, b) => a - b;

        private static readonly Func<int, int, int> MulIntFunc = (a, b) => a * b;
        private static readonly Func<double, double, double> MulDblFunc = (a, b) => a * b;

        private static readonly Func<int, int, int> DivIntFunc = (a, b) => a / b;
        private static readonly Func<double, double, double> DivDblFunc = (a, b) => a / b;

        private static NumberExpression BinaryOpNumbers
        (
            Expression l,
            Expression r,
            Func<int, int, int> opInt,
            Func<double, double, double> opDbl
        )
        {
            var left = new NumberExpression(l.StringValue);
            var right = new NumberExpression(r.StringValue);

            var result = left.IsInt && right.IsInt
                ? new NumberExpression(opInt.Invoke(left.ValueInt, right.ValueInt))
                : new NumberExpression(opDbl.Invoke(left.ValueDouble, right.ValueDouble));

            return result;
        }

        public static NumberExpression Sum(Expression l, Expression r)
        {
            return BinaryOpNumbers(l, r, SumIntFunc, SumDblFunc);
        }

        public static NumberExpression Sub(Expression l, Expression r)
        {
            return BinaryOpNumbers(l, r, SubIntFunc, SubDblFunc);
        }

        public static NumberExpression Mul(Expression l, Expression r)
        {
            return BinaryOpNumbers(l, r, MulIntFunc, MulDblFunc);
        }

        public static NumberExpression Div(Expression l, Expression r)
        {
            return BinaryOpNumbers(l, r, DivIntFunc, DivDblFunc);
        }

        public static BooleanExpression And(Expression l, Expression r)
        {
            return new BooleanExpression(l.StringValue == r.StringValue);
        }

        public static BooleanExpression Or(Expression l, Expression r)
        {
            return new BooleanExpression
            (
                string.Equals(l.StringValue, "True", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(r.StringValue, "True", StringComparison.OrdinalIgnoreCase)
            );
        }

        public static BooleanExpression Less(Expression l, Expression r)
        {
            var left = new NumberExpression(l.StringValue);
            var right = new NumberExpression(r.StringValue);

            var result = left.IsInt && right.IsInt
                ? new BooleanExpression(left.ValueInt < right.ValueInt)
                : new BooleanExpression(left.ValueDouble < right.ValueDouble);

            return result;
        }

        public static BooleanExpression Greater(Expression l, Expression r)
        {
            var left = new NumberExpression(l.StringValue);
            var right = new NumberExpression(r.StringValue);

            var result = left.IsInt && right.IsInt
                ? new BooleanExpression(left.ValueInt > right.ValueInt)
                : new BooleanExpression(left.ValueDouble > right.ValueDouble);

            return result;
        }

        public static BooleanExpression Equals(Expression l, Expression r)
        {
            var left = new NumberExpression(l.StringValue);
            var right = new NumberExpression(r.StringValue);

            var result = left.IsInt && right.IsInt
                ? new BooleanExpression(left.ValueInt == right.ValueInt)
                : new BooleanExpression(Math.Abs(left.ValueDouble - right.ValueDouble) <= double.Epsilon);

            return result;
        }
    }
}