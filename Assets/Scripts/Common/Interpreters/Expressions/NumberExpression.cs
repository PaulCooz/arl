namespace Common.Interpreters.Expressions
{
    public class NumberExpression : Expression
    {
        public readonly double Value;

        public NumberExpression(string value) : base(value)
        {
            Tools.ParseNumber(value, out Value);
        }

        public NumberExpression(double value) : base(value.ToString(Core.NumberFormat))
        {
            Value = value;
        }
    }
}