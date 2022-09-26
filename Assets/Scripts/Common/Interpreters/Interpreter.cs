using System.Collections.Generic;
using System.Text;

namespace Common.Interpreters
{
    public class Interpreter
    {
        private static readonly Dictionary<Token, int> OperationPrecedence = new()
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

        private readonly string _str;
        private int _index;

        private char _currentChar;
        private Token _prevToken;
        private Token _currentToken;

        private string _currentIdentifier;
        private string _currentNumber;

        public Value Value;

        private Token CurrentToken
        {
            get => _currentToken;
            set
            {
                _prevToken = _currentToken;
                _currentToken = value;
            }
        }

        public Interpreter(string s)
        {
            _str = s;
            _index = 0;
            _currentChar = NextChar();

            GetNextToken();

            var result = ParseExpression();
            Value = result != null ? new Value(result.StringValue) : Value.Null;
        }

        private char NextChar(bool withShift = true)
        {
            var nextChar = _index < _str.Length ? _str[_index] : '\0';
            if (withShift) _index++;
            return nextChar;
        }

        private void GetNextToken()
        {
            while (char.IsWhiteSpace(_currentChar)) _currentChar = NextChar();

            if (char.IsLetter(_currentChar))
            {
                var sb = new StringBuilder();
                do
                {
                    sb.Append(_currentChar);
                    _currentChar = _index < _str.Length ? _str[_index] : '\0';
                    _index++;
                } while (char.IsLetterOrDigit(_currentChar));

                _currentIdentifier = sb.ToString().ToLower();

                var id = _currentIdentifier.ToLower();
                switch (id)
                {
                    case "or":
                        CurrentToken = Token.Or;
                        return;

                    case "and":
                        CurrentToken = Token.And;
                        return;
                }

                CurrentToken = Token.Identifier;
                return;
            }

            if (Tools.IsDigitOrDot(_currentChar) ||
                (_currentChar == '-' && Tools.IsDigitOrDot(NextChar(false)) && _prevToken == Token.Number))
            {
                var number = new StringBuilder();
                do
                {
                    number.Append(_currentChar);
                    _currentChar = NextChar();
                } while (Tools.IsDigitOrDot(_currentChar) || _currentChar == 'f');

                _currentNumber = number.ToString();
                CurrentToken = Token.Number;
                return;
            }

            if (_currentChar == ':')
            {
                var operation = new StringBuilder();
                do
                {
                    operation.Append(_currentChar);
                    _currentChar = NextChar();
                } while (_currentChar == '=');

                if (operation.ToString() == ":=")
                {
                    CurrentToken = Token.Assignment;
                    return;
                }
            }

            CurrentToken = _currentChar switch
            {
                '+' => Token.Plus,
                '-' => Token.Minus,
                '*' => Token.Mult,
                '/' => Token.Div,

                '=' => Token.Equals,
                '<' => Token.Less,
                '>' => Token.Greater,

                '(' => Token.BrakeCirLeft,
                ')' => Token.BrakeCirRight,

                ';' => Token.Split,

                _ => Token.Exit
            };
            _currentChar = NextChar();
        }

        private int GetTokPrecedence()
        {
            return OperationPrecedence.ContainsKey(CurrentToken) ? OperationPrecedence[CurrentToken] : -1;
        }

        private Expression ParseIdentifierExpression()
        {
            var idName = _currentIdentifier;

            GetNextToken();

            if (CurrentToken != Token.BrakeCirLeft)
            {
                return ParseVariableExpression(idName);
            }

            GetNextToken(); // eat (
            var args = new List<Expression>();
            if (CurrentToken != Token.BrakeCirRight)
            {
                while (true)
                {
                    var arg = ParseExpression();
                    if (arg != null)
                    {
                        args.Add(arg);
                    }
                    else
                    {
                        return null;
                    }

                    if (CurrentToken == Token.BrakeCirRight)
                        break;

                    if (CurrentToken != Token.Split)
                        return null;

                    GetNextToken();
                }
            }

            GetNextToken(); // eat )

            return new CallExpression(idName, args);
        }

        private Expression ParseVariableExpression(string idName)
        {
            if (CurrentToken != Token.Assignment)
            {
                return Context.GetVariable(idName);
            }

            GetNextToken(); // eat :=

            var variableExpression = new VariableExpression(idName, ParseExpression());
            Context.SetVariable(variableExpression);

            return variableExpression;
        }

        private Expression ParseNumberExpression()
        {
            var result = new NumberExpression(_currentNumber);
            GetNextToken();

            return result;
        }

        private Expression ParseParenExpr()
        {
            GetNextToken(); // eat (.
            var expression = ParseExpression();
            if (expression == null || CurrentToken != Token.BrakeCirRight)
            {
                return null;
            }

            GetNextToken(); // eat ).
            return expression;
        }


        private Expression ParsePrimary()
        {
            switch (CurrentToken)
            {
                case Token.Identifier:
                    return ParseIdentifierExpression();

                case Token.Number:
                    return ParseNumberExpression();

                case Token.BrakeCirLeft:
                    return ParseParenExpr();

                default:
                    return null;
            }
        }

        private Expression ParseBinOpRhs(int expressionPrecedence, Expression lhs)
        {
            while (true)
            {
                var tokPrecedence = GetTokPrecedence();
                if (tokPrecedence < expressionPrecedence) return lhs;

                var binOp = CurrentToken;
                GetNextToken(); // eat operator

                var rhs = ParsePrimary();
                if (rhs == null) return null;

                var nextPrecedence = GetTokPrecedence();
                if (tokPrecedence < nextPrecedence)
                {
                    rhs = ParseBinOpRhs(tokPrecedence + 1, rhs);
                    if (rhs == null) return null;
                }

                lhs = new BinaryExpression(binOp, lhs, rhs).Expression;
            }
        }

        private Expression ParseExpression()
        {
            var lhs = ParsePrimary();
            return lhs == null ? null : ParseBinOpRhs(0, lhs);
        }
    }
}