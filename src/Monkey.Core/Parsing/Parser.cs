using Monkey.Core.Lexing;
using System.Collections.Generic;
using Monkey.Core.Parsing.Statements;

namespace Monkey.Core.Parsing
{
    public class Parser
    {
        private readonly Lexer _lexer;
        private ParserTokens _parserTokens;

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
                        statement = ParseExpressionStatement();
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

        private IStatement ParseExpressionStatement() 
            => new ExpressionFactory().GetExpression(this);

        private IStatement NullStatement(Token newToken, string errorMessage)
        {
            while (newToken.TokenType != TokenType.SEMICOLON)
                newToken = _parserTokens.NextToken();
            return new NullStatement(errorMessage);
        }

        public Token PeekToken() 
            => _parserTokens.PeekToken();

        public Token NextToken() 
            => _parserTokens.NextToken();

        public Token CurrentToken() 
            => _parserTokens.CurrentToken();

        private Queue<Token> ParseScriptIntoQueue(string scriptText) 
            => new Queue<Token>(_lexer.ParseScript(scriptText));
    }
}