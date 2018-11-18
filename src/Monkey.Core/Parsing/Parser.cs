using System;
using Monkey.Core.Lexing;
using System.Collections.Generic;
using Monkey.Core.Parsing.Expressions;
using Monkey.Core.Parsing.Statements;

namespace Monkey.Core.Parsing
{
    public class Parser
    {
        private readonly Lexer _lexer;
        private ParserTokens _parserTokens;

        private readonly Dictionary<TokenType, Func<Token, IExpression>> _parsePrefixes
            = new Dictionary<TokenType, Func<Token, IExpression>>
            {
                {TokenType.IDENT, ParseIdentifier},
                {TokenType.INT, ParseInteger}
            };
        
        private Dictionary<TokenType, Func<IExpression>> _parseInfix
            = new Dictionary<TokenType, Func<IExpression>>();

        public Parser() 
            => _lexer = new Lexer();

        public IProgram Parse(string scriptText)
        {
            var tokens = ParseScriptIntoQueue(scriptText);
            var statements = new List<IStatement>();
            _parserTokens = new ParserTokens(tokens);

            var currentToken = _parserTokens.NextToken();
            while (currentToken.TokenType != TokenType.EOF)
            {
                IStatement statement;
                switch (currentToken.TokenType)
                {
                    case TokenType.LET:
                    {
                        statement = ParseLetStatement(currentToken); 
                        break;
                    }
                    case TokenType.RETURN:
                    {
                        statement = ParseReturnStatement(currentToken);
                        break;
                    }
                    default:
                    {
                        statement = ParseExpressionStatement(currentToken);
                        break;
                    }
                }

                statements.Add(statement);
                currentToken = _parserTokens.NextToken();
            }
            
            return new Program(statements);
        }

        private IStatement ParseReturnStatement(Token currentToken)
        {
            var statement = new ReturnStatement(currentToken);
            var newToken = _parserTokens.NextToken();

            while (newToken.TokenType != TokenType.SEMICOLON)
                newToken = _parserTokens.NextToken();
            return statement;
        }

        private IStatement ParseLetStatement(Token currentToken)
        {
            var statement = new LetStatement(currentToken);

            var peekToken = _parserTokens.PeekToken();
            if (peekToken.TokenType != TokenType.IDENT)
                return NullStatement(currentToken, string.Empty);

            var newToken = _parserTokens.NextToken();
            statement.Name = newToken.Value;
            
            newToken = _parserTokens.NextToken();
            if (newToken.TokenType != TokenType.ASSIGN)
                return NullStatement(newToken, $"Expected '=' but have {newToken.TokenType}");

            while (newToken.TokenType != TokenType.SEMICOLON)
                newToken = _parserTokens.NextToken();
            return statement;
        }

        private IStatement ParseExpressionStatement(Token currentToken)
        {
            var expressionStatement = new ExpressionStatement(currentToken)
            {
                Expression = ParseExpression(currentToken, Priority.Lowest)
            };

            var newToken = _parserTokens.PeekToken();
            if (newToken.TokenType == TokenType.SEMICOLON)
                _parserTokens.NextToken();
            
            return expressionStatement;
        }

        private IExpression ParseExpression(Token currentToken, Priority priority)
        {
            if (_parsePrefixes.ContainsKey(currentToken.TokenType))
                return _parsePrefixes[currentToken.TokenType](currentToken);
            return null;
        }

        private IStatement NullStatement(Token newToken, string errorMessage)
        {
            while (newToken.TokenType != TokenType.SEMICOLON)
                newToken = _parserTokens.NextToken();
            return new NullStatement(errorMessage);
        }

        private static IExpression ParseIdentifier(Token currentToken) 
            => new Identifier(currentToken);

        private static IExpression ParseInteger(Token currentToken)
        {
            var expression = new IntegerStatement(currentToken);
            if (int.TryParse(currentToken.Value, out var value))
                expression.Value = value;
            
            return expression;
        }

        private Queue<Token> ParseScriptIntoQueue(string scriptText) 
            => new Queue<Token>(_lexer.ParseScript(scriptText));
    }

    public enum Priority
    {
        Lowest
    }
}