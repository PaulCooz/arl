namespace Common.Interpreters.Expressions
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
}