using Monkey.Core.Lexing;
using Monkey.Core.Parsing.Statements;

namespace Monkey.Core.Parsing.Expressions
{
    public class IntegerStatement : IExpression
    {
        private readonly Token _currentToken;

        public IntegerStatement(Token currentToken)
        {
            _currentToken = currentToken;
        }

        public string TokenLiteral() 
            => _currentToken.Value;

        public int Value { get; set; }
    }
}