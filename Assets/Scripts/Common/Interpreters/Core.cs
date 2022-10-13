using System.Collections.Generic;
using System.Globalization;
using Common.Interpreters.Expressions;

namespace Common.Interpreters
{
    public static class Core
    {
        public enum Token
        {
            Exit,

            Identifier,
            Number,
            String,

            Plus, // +
            Minus, // -
            Mult, // *
            Div, // /

            Equals, // =
            Less, // <
            Greater, // >
            And, // and
            Or, // or

            Split, // ;

            BrakeCirLeft, // (
            BrakeCirRight, // )

            BrakeSqrLeft, // [
            BrakeSqrRight, // ]

            Quotes, // " or '

            Assignment // :=
        }

        public delegate Expression Function(in IReadOnlyList<Expression> expressions);

        public delegate Expression BinOperation(in Expression left, in Expression right);

        public static readonly Dictionary<Token, int> OperationPrecedence = new()
        {
            {Token.And, 5},
            {Token.Or, 5},

            {Token.Equals, 10},
            {Token.Less, 10},
            {Token.Greater, 10},

            {Token.Plus, 20},
            {Token.Minus, 20},

            {Token.Mult, 40},
            {Token.Div, 40}
        };

        public static readonly NumberFormatInfo NumberFormat = new() {NumberDecimalDigits = '.'};
    }
}