using System.Text.RegularExpressions;

namespace Common.Interpreters.Expressions
{
    public class BooleanExpression : Expression
    {
        private static readonly Regex BooleanTruePattern = new("^(true)$", RegexOptions.IgnoreCase);

        public bool Value => BooleanTruePattern.IsMatch(StringValue);

        public BooleanExpression(bool value) : base(value.ToString()) { }
        public BooleanExpression(string value) : base(BooleanTruePattern.IsMatch(value).ToString()) { }
    }
}