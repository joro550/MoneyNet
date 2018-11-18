using Monkey.Core.Lexing;
using Monkey.Core.Parsing.Statements;

namespace Monkey.Core.Parsing.Expressions
{
    public class Identifier : IExpression
    {
        private readonly Token _currentToken;

        public Identifier(Token currentToken)
        {
            _currentToken = currentToken;
        }

        public string TokenLiteral() 
            => _currentToken.Value;

    }
}