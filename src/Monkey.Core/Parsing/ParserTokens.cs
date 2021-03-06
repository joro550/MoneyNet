    using System.Collections.Generic;
using Monkey.Core.Lexing;

namespace Monkey.Core.Parsing
{
    internal class ParserTokens
    {
        private readonly Queue<Token> _tokens;
        private Token _currentToken;

        public ParserTokens(Queue<Token> tokens) 
            => _tokens = tokens;

        public Token NextToken()
        {
            _tokens.TryDequeue(out var t);
            _currentToken = t;
            return t;
        }

        public Token PeekToken()
        {
            _tokens.TryPeek(out var t);
            return t;
        }

        public Token CurrentToken() 
            => _currentToken;
    }
}