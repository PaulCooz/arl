using System.Collections.Generic;

namespace Common.Interpreters
{
    public class Context
    {
        private static readonly IDictionary<string, Core.Function> Functions;
        private static readonly IDictionary<string, Expression> Variables;
        private static readonly IDictionary<Core.Token, Core.BinOperation> Operations;

        static Context()
        {
            Variables = new Dictionary<string, Expression>();
            Functions = new Dictionary<string, Core.Function>();
            Operations = new Dictionary<Core.Token, Core.BinOperation>();

            StandardContext.Fill(Functions, Variables, Operations);
        }

        public static Expression GetVariable(in string name)
        {
            return Variables.ContainsKey(name) ? Variables[name] : new Expression(name);
        }

        public static Core.BinOperation GetOperation(in Core.Token token)
        {
            return Operations[token];
        }

        public static Core.Function GetFunction(in string name)
        {
            return Functions[name];
        }

        public static void SetVariable(in string name, in Expression expression)
        {
            if (Variables.ContainsKey(name))
            {
                Variables[name] = expression;
            }
            else
            {
                Variables.Add(name, expression);
            }
        }

        public static void SetVariable(in VariableExpression variable)
        {
            SetVariable(variable.Name, variable.Value);
        }

        public static void SetOperation(in Core.Token token, in Core.BinOperation binOperation)
        {
            if (Operations.ContainsKey(token))
            {
                Operations[token] = binOperation;
            }
            else
            {
                Operations.Add(token, binOperation);
            }
        }

        public static void SetFunction(in string name, in Core.Function function)
        {
            if (Functions.ContainsKey(name))
            {
                Functions[name] = function;
            }
            else
            {
                Functions.Add(name, function);
            }
        }
    }
}