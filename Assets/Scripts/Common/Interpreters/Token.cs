namespace Common.Interpreters
{
    public enum Token
    {
        Exit,

        Identifier,
        Number,

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

        Assignment // :=
    }
}