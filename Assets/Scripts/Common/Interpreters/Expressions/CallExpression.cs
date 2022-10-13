using System.Collections.Generic;

namespace Common.Interpreters.Expressions
{
    public class CallExpression : Expression
    {
        public CallExpression(string name, IReadOnlyList<Expression> args, in Context context)
            : base(CallMethod(name, args, context)) { }

        private static string CallMethod(in string name, in IReadOnlyList<Expression> args, in Context context)
        {
            return context.GetFunction(name).Invoke(args).StringValue;
        }
    }
}