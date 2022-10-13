namespace Common.Interpreters.Expressions
{
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
}