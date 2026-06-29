using System;
using System.Collections.Generic;
using LogicExpressions;
using ParsedTruthTerm = LogicExpressions.TruthTerm;

namespace Input;

/// <summary>
/// Reads and parses logical expressions into truth-term objects.
/// </summary>
public sealed class TruthTermInputModule
{
    /// <summary>
    /// Reads one expression line from the console and parses it.
    /// </summary>
    /// <returns>Parsed truth term.</returns>
    public ParsedTruthTerm ReadFromConsole()
    {
        string? input = Console.ReadLine();

        return Parse(input);
    }

    /// <summary>
    /// Parses an input expression into a truth-term object.
    /// </summary>
    /// <param name="input">Raw input expression.</param>
    /// <returns>Parsed truth term.</returns>
    public ParsedTruthTerm Parse(string? input)
    {
        var parser = new Parser(input!);
        IExpressionNode rootClause = parser.ParseExpression();

        return new ParsedTruthTerm(input!, rootClause);
    }

    /// <summary>
    /// Recursive-descent parser for logical expressions.
    /// </summary>
    private sealed class Parser
    {
        private readonly List<string> _tokens;
        private int _index;

        /// <summary>
        /// Initializes the parser with tokenized input.
        /// </summary>
        /// <param name="input">Expression text to parse.</param>
        public Parser(string input)
        {
            _tokens = Tokenize(input);
            _index = 0;
        }

        /// <summary>
        /// Parses the full expression and validates token exhaustion.
        /// </summary>
        /// <returns>Root node of the parsed expression tree.</returns>
        public IExpressionNode ParseExpression()
        {
            IExpressionNode result = ParseOr();

            if (!IsAtEnd())
            {
                throw new ArgumentException($"Unexpected token '{Peek()}'.");
            }

            return result;
        }

        /// <summary>
        /// Parses OR-precedence expressions.
        /// </summary>
        /// <returns>Parsed expression node.</returns>
        private IExpressionNode ParseOr()
        {
            IExpressionNode left = ParseAnd();

            while (MatchOperator("OR"))
            {
                IExpressionNode right = ParseAnd();
                left = new LogicClause(LogicOperator.Or, left, right);
            }

            return left;
        }

        /// <summary>
        /// Parses AND-precedence expressions.
        /// </summary>
        /// <returns>Parsed expression node.</returns>
        private IExpressionNode ParseAnd()
        {
            IExpressionNode left = ParseUnary();

            while (true)
            {
                if (MatchOperator("AND"))
                {
                    IExpressionNode right = ParseUnary();
                    left = new LogicClause(LogicOperator.And, left, right);
                    continue;
                }

                if (MatchOperator("XOR"))
                {
                    throw new NotImplementedException("XOR is not implemented.");
                }

                if (MatchOperator("NAND"))
                {
                    throw new NotImplementedException("NAND is not implemented.");
                }

                if (MatchOperator("NOR"))
                {
                    throw new NotImplementedException("NOR is not implemented.");
                }

                break;
            }

            return left;
        }

        /// <summary>
        /// Parses unary expressions, currently only NOT.
        /// </summary>
        /// <returns>Parsed expression node.</returns>
        private IExpressionNode ParseUnary()
        {
            if (MatchOperator("NOT"))
            {
                IExpressionNode operand = ParseUnary();
                return new LogicClause(LogicOperator.Not, operand);
            }

            return ParsePrimary();
        }

        /// <summary>
        /// Parses primary expressions: parenthesized groups or variables.
        /// </summary>
        /// <returns>Parsed expression node.</returns>
        private IExpressionNode ParsePrimary()
        {
            if (Match("("))
            {
                IExpressionNode expression = ParseOr();

                if (!Match(")"))
                {
                    throw new ArgumentException("Missing closing parenthesis.");
                }

                return expression;
            }

            if (IsAtEnd())
            {
                throw new ArgumentException("Unexpected end of expression.");
            }

            string token = Advance();

            if (LogicTokens.IsIdentifier(token))
            {
                return new Variable(token);
            }

            throw new ArgumentException($"Invalid token '{token}'.");
        }

        /// <summary>
        /// Splits raw input into parser tokens.
        /// </summary>
        /// <param name="input">Expression text.</param>
        /// <returns>Token sequence.</returns>
        private static List<string> Tokenize(string input)
        {
            var tokens = new List<string>();
            int i = 0;

            while (i < input.Length)
            {
                char c = input[i];

                if (char.IsWhiteSpace(c))
                {
                    i++;
                    continue;
                }

                if (c == '(' || c == ')')
                {
                    tokens.Add(c.ToString());
                    i++;
                    continue;
                }

                if (char.IsLetter(c))
                {
                    int start = i;
                    i++;

                    while (i < input.Length && (char.IsLetterOrDigit(input[i]) || input[i] == '_'))
                    {
                        i++;
                    }

                    string token = input[start..i].ToUpperInvariant();
                    tokens.Add(token);
                    continue;
                }

                throw new ArgumentException($"Invalid character '{c}'.");
            }

            return tokens;
        }

        /// <summary>
        /// Matches and consumes an operator token.
        /// </summary>
        /// <param name="expected">Expected operator token.</param>
        /// <returns><c>true</c> if the token matched.</returns>
        private bool MatchOperator(string expected)
        {
            if (IsAtEnd())
            {
                return false;
            }

            if (Peek() != expected)
            {
                return false;
            }

            Advance();
            return true;
        }

        /// <summary>
        /// Matches and consumes an arbitrary token.
        /// </summary>
        /// <param name="expected">Expected token.</param>
        /// <returns><c>true</c> if the token matched.</returns>
        private bool Match(string expected)
        {
            if (IsAtEnd())
            {
                return false;
            }

            if (Peek() != expected)
            {
                return false;
            }

            Advance();
            return true;
        }

        /// <summary>
        /// Returns the current token and advances the parser index.
        /// </summary>
        /// <returns>Consumed token.</returns>
        private string Advance()
        {
            string current = _tokens[_index];
            _index++;
            return current;
        }

        /// <summary>
        /// Returns the current token without consuming it.
        /// </summary>
        /// <returns>Current token.</returns>
        private string Peek()
        {
            return _tokens[_index];
        }

        /// <summary>
        /// Indicates whether all tokens have been consumed.
        /// </summary>
        /// <returns><c>true</c> when no tokens remain.</returns>
        private bool IsAtEnd()
        {
            return _index >= _tokens.Count;
        }
    }
}
