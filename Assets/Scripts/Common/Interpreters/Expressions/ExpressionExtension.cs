using System.Text.RegularExpressions;

namespace Common.Interpreters.Expressions
{
    public static class ExpressionExtension
    {
        private static readonly Regex BooleanPattern = new("^(true|false)$", RegexOptions.IgnoreCase);
        private static readonly Regex NumberPattern = new(@"^\d*(,|.)?\d*$");
        private static readonly Regex StringPattern = new("^('|\").*('|\")$");
        private static readonly Regex ArrayPattern = new(@"^\[.*\]$");

        public static bool IsNumber(this Expression expression) => NumberPattern.IsMatch(expression.StringValue);
        public static bool IsString(this Expression expression) => StringPattern.IsMatch(expression.StringValue);
        public static bool IsBoolean(this Expression expression) => BooleanPattern.IsMatch(expression.StringValue);
        public static bool IsArray(this Expression expression) => ArrayPattern.IsMatch(expression.StringValue);
    }
}