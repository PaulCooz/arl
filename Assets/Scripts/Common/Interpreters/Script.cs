﻿using System;
using System.Collections.Generic;

namespace Common.Interpreters
{
    public class Script
    {
        private readonly Context _context;

        public Script()
        {
            _context = new Context();
        }

        public Value Run(in string str)
        {
            return new Interpreter(str, _context).Value;
        }

        public void SetVariable(in string name, in string value)
        {
            _context.SetVariable(name, new Expression(value));
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