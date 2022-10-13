namespace Common.Interpreters.Expressions
{
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
}