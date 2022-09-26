using System.Collections.Generic;

namespace Common.Interpreters
{
    public class Context
    {
        private static readonly IDictionary<string, Expression> Variables;

        static Context()
        {
            Variables = new Dictionary<string, Expression>();
        }

        public static void SetVariable(in string name, in Expression expression)
        {
            if (!Variables.ContainsKey(name))
            {
                Variables.Add(name, expression);
            }
            else
            {
                Variables[name] = expression;
            }
        }

        public static void SetVariable(in VariableExpression variable)
        {
            SetVariable(variable.Name, variable.Value);
        }

        public static Expression GetVariable(in string name)
        {
            return Variables.ContainsKey(name) ? Variables[name] : new Expression(name);
        }
    }
}