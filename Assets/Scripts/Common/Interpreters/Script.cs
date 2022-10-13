using System;
using System.Collections.Generic;
using Common.Interpreters.Expressions;

namespace Common.Interpreters
{
    public class Script
    {
        private readonly Context _context;

        public Script()
        {
            _context = new Context();
        }

        public Script(Context context)
        {
            _context = context;
        }

        public Value Run(in string str)
        {
            return new Interpreter(str, _context).Value;
        }

        public void SetVariable(in string name, in Value value)
        {
            _context.SetVariable(name, new Expression(value.ScriptValue));
        }

        public void SetProperty(in string name, Func<Value> get, Action<Value> set)
        {
            _context.SetFunction
            (
                $"get_{name}",
                (in IReadOnlyList<Expression> _) => new Expression(get.Invoke().ScriptValue)
            );
            _context.SetFunction
            (
                $"set_{name}",
                (in IReadOnlyList<Expression> expressions) =>
                {
                    set.Invoke(new Value(expressions[0].StringValue));
                    return Expression.Empty;
                }
            );
        }
    }
}