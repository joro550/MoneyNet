using System;
using System.Collections.Generic;
using System.Linq;
using Monkey.Core.Lexing;

namespace Monkey.Core.Parsing
{
    public class Parser
    {
        private readonly Dictionary<TokenType, Func<TokenCollection, IStatement>> TokenToStatementMap 
            = new Dictionary<TokenType, Func<TokenCollection, IStatement>>
        {
            {TokenType.LET, LetStatement.CreateFromTokens},
            {TokenType.RETURN, ReturnStatement.CreateFromTokens}
        };
        
        
        public Program ParseTokens(IEnumerable<Token> tokens)
        {
            var tokenCollection = new TokenCollection(tokens.ToList());
            var program = new Program();

            var currentToken = tokenCollection.Current();
            while (currentToken.TokenType != TokenType.EOF)
            {
                if (TokenToStatementMap.ContainsKey(currentToken.TokenType))
                {
                    var statement = TokenToStatementMap[currentToken.TokenType](tokenCollection);
                    program.AddStatement(statement);
                }

                currentToken = tokenCollection.Next();
            }

            return program;
        }
    }

    public class TokenCollection
    {
        private readonly List<Token> _tokens;
        private int _currentPosition;
        
        public TokenCollection(List<Token> tokens)
        {
            _tokens = tokens;
        }

        public Token Current() 
            => _tokens[_currentPosition];

        public Token Peek() 
            => _tokens[_currentPosition + 1];

        public Token Next()
        {
            _currentPosition++;
            return Current();
        }
    }
}