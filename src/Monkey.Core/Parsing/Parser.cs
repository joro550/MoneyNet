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
                IStatement statement = new NullStatement();
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
                return new NullStatement();

            var newToken = _parserTokens.NextToken();
            statement.Name = newToken.Value;

            while (newToken.TokenType != TokenType.SEMICOLON)
                newToken = _parserTokens.NextToken();
            return statement;
        }

        private Queue<Token> ParseScriptIntoQueue(string scriptText) 
            => new Queue<Token>(_lexer.ParseScript(scriptText));
    }
}