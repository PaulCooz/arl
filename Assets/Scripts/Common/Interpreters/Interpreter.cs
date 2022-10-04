﻿using System.Collections.Generic;
using System.Text;

namespace Common.Interpreters
{
    public class Interpreter
    {
        private readonly Context _context;

        private string _str;
        private int _index;

        private char _currentChar;
        private Core.Token _prevToken;
        private Core.Token _currentToken;

        private string _currentIdentifier;
        private string _currentNumber;
        private string _currentString;

        public Value Value;

        private Core.Token CurrentToken
        {
            get => _currentToken;
            set
            {
                _prevToken = _currentToken;
                _currentToken = value;
            }
        }

        public Interpreter(string s, Context context)
        {
            _context = context;

            var lines = s.Split('\n');
            foreach (var line in lines)
            {
                Execute(line);
            }
        }

        private void Execute(string code)
        {
            _str = code;
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

            if (GetString()) return;
            if (GetIdentifier()) return;
            if (GetNumber()) return;
            if (GetAssignment()) return;

            CurrentToken = _currentChar switch
            {
                '+' => Core.Token.Plus,
                '-' => Core.Token.Minus,
                '*' => Core.Token.Mult,
                '/' => Core.Token.Div,

                '=' => Core.Token.Equals,
                '<' => Core.Token.Less,
                '>' => Core.Token.Greater,

                '(' => Core.Token.BrakeCirLeft,
                ')' => Core.Token.BrakeCirRight,

                '[' => Core.Token.BrakeSqrLeft,
                ']' => Core.Token.BrakeSqrRight,

                ';' => Core.Token.Split,

                _ => Core.Token.Exit
            };
            _currentChar = NextChar();
        }

        private bool GetString()
        {
            if (!Tools.IsQuote(_currentChar)) return false;

            var sb = new StringBuilder();
            do
            {
                sb.Append(_currentChar);
                _currentChar = _index < _str.Length ? _str[_index] : '\0';
                _index++;
            } while (!Tools.IsQuote(_currentChar));

            sb.Append(_currentChar);
            _currentChar = _index < _str.Length ? _str[_index] : '\0';
            _index++;

            _currentString = sb.ToString();
            CurrentToken = Core.Token.String;
            return true;
        }

        private bool GetIdentifier()
        {
            if (!char.IsLetter(_currentChar) && _currentChar != '_') return false;

            var sb = new StringBuilder();
            do
            {
                sb.Append(_currentChar);
                _currentChar = _index < _str.Length ? _str[_index] : '\0';
                _index++;
            } while (char.IsLetterOrDigit(_currentChar) || _currentChar == '_');

            _currentIdentifier = sb.ToString().ToLower();

            var id = _currentIdentifier.ToLower();
            switch (id)
            {
                case "or":
                    CurrentToken = Core.Token.Or;
                    return true;

                case "and":
                    CurrentToken = Core.Token.And;
                    return true;
            }

            CurrentToken = Core.Token.Identifier;
            return true;
        }

        private bool GetAssignment()
        {
            if (_currentChar != ':') return false;

            var operation = new StringBuilder();
            do
            {
                operation.Append(_currentChar);
                _currentChar = NextChar();
            } while (_currentChar == '=');

            if (operation.ToString() == ":=")
            {
                CurrentToken = Core.Token.Assignment;
                return true;
            }

            return false;
        }

        private bool GetNumber()
        {
            var prefixCheck = _currentChar != '-' || !Tools.IsNumberPart(NextChar(false)) || _prevToken != Core.Token.Number;
            if (!Tools.IsNumberPart(_currentChar) && prefixCheck)
            {
                return false;
            }

            var number = new StringBuilder();
            var prevE = false;
            do
            {
                number.Append(_currentChar);
                prevE = _currentChar is 'e' or 'E';
                _currentChar = NextChar();
            } while (Tools.IsNumberPart(_currentChar) || (prevE && _currentChar is '-' or '+'));

            _currentNumber = number.ToString();
            CurrentToken = Core.Token.Number;
            return true;
        }

        private int GetTokPrecedence()
        {
            return Core.OperationPrecedence.ContainsKey(CurrentToken) ? Core.OperationPrecedence[CurrentToken] : -1;
        }

        private Expression ParseIdentifierExpression()
        {
            var idName = _currentIdentifier;

            GetNextToken();

            if (CurrentToken != Core.Token.BrakeCirLeft)
            {
                return ParseVariableExpression(idName);
            }

            GetNextToken(); // eat (
            var args = new List<Expression>();
            if (CurrentToken != Core.Token.BrakeCirRight)
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

                    if (CurrentToken == Core.Token.BrakeCirRight)
                        break;

                    if (CurrentToken != Core.Token.Split)
                        return null;

                    GetNextToken();
                }
            }

            GetNextToken(); // eat )

            return new CallExpression(idName, args, _context);
        }

        private Expression ParseVariableExpression(string idName)
        {
            if (CurrentToken != Core.Token.Assignment)
            {
                return _context.GetVariable(idName);
            }

            GetNextToken(); // eat :=

            var variableExpression = new VariableExpression(idName, ParseExpression());
            _context.SetVariable(variableExpression);

            return variableExpression;
        }

        private Expression ParseNumberExpression()
        {
            var result = new NumberExpression(_currentNumber);
            GetNextToken();

            return result;
        }

        private Expression ParseStringExpression()
        {
            var result = new StringExpression(_currentString);
            GetNextToken();

            return result;
        }

        private Expression ParseParenExpr()
        {
            GetNextToken(); // eat (.
            var expression = ParseExpression();
            if (expression == null || CurrentToken != Core.Token.BrakeCirRight)
            {
                return null;
            }

            GetNextToken(); // eat ).
            return expression;
        }

        private Expression ParseArrayExpression()
        {
            GetNextToken(); // eat [
            var elements = new List<Expression>();
            if (CurrentToken != Core.Token.BrakeSqrRight)
            {
                while (true)
                {
                    var arg = ParseExpression();
                    if (arg != null)
                    {
                        elements.Add(arg);
                    }
                    else
                    {
                        return null;
                    }

                    if (CurrentToken == Core.Token.BrakeSqrRight)
                        break;

                    if (CurrentToken != Core.Token.Split)
                        return null;

                    GetNextToken();
                }
            }

            GetNextToken(); // eat ]

            return new ArrayExpression(elements);
        }

        private Expression ParsePrimary()
        {
            switch (CurrentToken)
            {
                case Core.Token.Identifier:
                    return ParseIdentifierExpression();

                case Core.Token.Number:
                    return ParseNumberExpression();

                case Core.Token.String:
                    return ParseStringExpression();

                case Core.Token.BrakeCirLeft:
                    return ParseParenExpr();

                case Core.Token.BrakeSqrLeft:
                    return ParseArrayExpression();

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

                lhs = new BinaryExpression(binOp, lhs, rhs, _context);
            }
        }

        private Expression ParseExpression()
        {
            var lhs = ParsePrimary();
            return lhs == null ? null : ParseBinOpRhs(0, lhs);
        }
    }
}