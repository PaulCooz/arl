using System.Collections.Generic;

namespace Common.Interpreters.Expressions
{
    public class ArrayExpression : Expression
    {
        public ArrayExpression(IEnumerable<Expression> elements) : base(elements.ToStrArray()) { }
    }
}