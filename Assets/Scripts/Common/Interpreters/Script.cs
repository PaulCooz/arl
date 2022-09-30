using System;
using System.Collections.Generic;

namespace Common.Interpreters
{
    public class Script
    {
        public readonly Context Context;

        public Script()
        {
            Context = new Context();
        }

        public Script(Context context)
        {
            Context = context;
        }

        public Value Run(string str)
        {
            return new Interpreter(str, Context).Value;
        }

        public void AddVariable(string name, string value)
        {
            Context.SetVariable(name, new Expression(value));
        }

        public void AddProperty(string name, Func<string> get, Action<string> set)
        {
            Context.SetFunction
            (
                $"get_{name}",
                (in IReadOnlyList<Expression> _) => new Expression(get.Invoke())
            );
            Context.SetFunction
            (
                $"set_{name}",
                (in IReadOnlyList<Expression> expressions) =>
                {
                    set.Invoke(expressions[0].StringValue);
                    return Expression.Empty;
                }
            );
        }
    }
}