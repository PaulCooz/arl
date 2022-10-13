using System.Collections.Generic;
using Common.Interpreters.Expressions;

namespace Common.Interpreters
{
    public class Context
    {
        private static readonly IDictionary<string, Core.Function> Functions;
        private static readonly IDictionary<string, Expression> Variables;
        private static readonly IDictionary<Core.Token, Core.BinOperation> Operations;

        private readonly IDictionary<string, Core.Function> _localFunctions;
        private readonly IDictionary<string, Expression> _localVariables;
        private readonly IDictionary<Core.Token, Core.BinOperation> _localOperations;

        static Context()
        {
            Variables = new Dictionary<string, Expression>();
            Functions = new Dictionary<string, Core.Function>();
            Operations = new Dictionary<Core.Token, Core.BinOperation>();

            StandardContext.Fill(Functions, Variables, Operations);
        }

        public Context()
        {
            _localVariables = new Dictionary<string, Expression>();
            _localFunctions = new Dictionary<string, Core.Function>();
            _localOperations = new Dictionary<Core.Token, Core.BinOperation>();
        }

        #region Global

        public static Expression GetGlobalVariable(in string name)
        {
            return Variables.ContainsKey(name) ? Variables[name] : new Expression(name);
        }

        public static Core.BinOperation GetGlobalOperation(in Core.Token token)
        {
            return Operations[token];
        }

        public static Core.Function GetGlobalFunction(in string name)
        {
            return Functions[name];
        }

        public static void SetGlobalVariable(in string name, in Expression expression)
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

        public static void SetGlobalVariable(in VariableExpression variable)
        {
            SetGlobalVariable(variable.Name, variable.Value);
        }

        public static void SetGlobalVariable(in string name, in Value value)
        {
            SetGlobalVariable(name, new Expression(value.ScriptValue));
        }

        public static void SetGlobalOperation(in Core.Token token, in Core.BinOperation binOperation)
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

        public static void SetGlobalFunction(in string name, in Core.Function function)
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

        #endregion // Global

        #region Local

        public Expression GetVariable(in string name)
        {
            return _localVariables.ContainsKey(name) ? _localVariables[name] : GetGlobalVariable(name);
        }

        public Core.BinOperation GetOperation(in Core.Token token)
        {
            return _localOperations.ContainsKey(token) ? _localOperations[token] : GetGlobalOperation(token);
        }

        public Core.Function GetFunction(in string name)
        {
            return _localFunctions.ContainsKey(name) ? _localFunctions[name] : GetGlobalFunction(name);
        }

        public void SetVariable(in string name, in Expression expression)
        {
            if (_localVariables.ContainsKey(name))
            {
                _localVariables[name] = expression;
            }
            else
            {
                _localVariables.Add(name, expression);
            }
        }

        public void SetVariable(in VariableExpression variable)
        {
            SetVariable(variable.Name, variable.Value);
        }

        public void SetOperation(in Core.Token token, in Core.BinOperation binOperation)
        {
            if (_localOperations.ContainsKey(token))
            {
                _localOperations[token] = binOperation;
            }
            else
            {
                _localOperations.Add(token, binOperation);
            }
        }

        public void SetFunction(in string name, in Core.Function function)
        {
            if (_localFunctions.ContainsKey(name))
            {
                _localFunctions[name] = function;
            }
            else
            {
                _localFunctions.Add(name, function);
            }
        }

        #endregion // Local

    }
}