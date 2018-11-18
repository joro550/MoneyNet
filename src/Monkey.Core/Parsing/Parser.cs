using System.Collections.Generic;
using Monkey.Core.Lexing;

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
                IStatement statement = new NullStatement(string.Empty);
                switch (currentToken.TokenType)
                {
                    case TokenType.LET:
                    {
                        statement = ParseLetStatement(currentToken); 
                        break;
                    }
                }

                statements.Add(statement);
                currentToken = _parserTokens.NextToken();
            }
            
            return new Program(statements);
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
            if (newToken.TokenType != TokenType.EQ)
                return NullStatement(newToken, $"Expected '=' but have {newToken.TokenType}");

            while (newToken.TokenType != TokenType.SEMICOLON)
                newToken = _parserTokens.NextToken();
            return statement;
        }

        private IStatement NullStatement(Token newToken, string errorMessage)
        {
            while (newToken.TokenType != TokenType.SEMICOLON)
                newToken = _parserTokens.NextToken();
            return new NullStatement(errorMessage);
        }

        private Queue<Token> ParseScriptIntoQueue(string scriptText) 
            => new Queue<Token>(_lexer.ParseScript(scriptText));
    }
}