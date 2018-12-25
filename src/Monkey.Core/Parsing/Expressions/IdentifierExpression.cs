using Monkey.Core.Lexing;
using Monkey.Core.Parsing.Statements;

namespace Monkey.Core.Parsing.Expressions
{
    public class IdentifierExpression : IExpression
    {
        private readonly Token _currentToken;

        private IdentifierExpression(Token currentToken)
        {
            _currentToken = currentToken;
        }

        public string TokenLiteral() 
            => _currentToken.Value;

        public static IExpression Parse(ExpressionFactory factory, Parser parser) 
            => new IdentifierExpression(parser.CurrentToken());

        public static IExpression Parse(IExpression left, ExpressionFactory factory, Parser parser)
        {
            throw new System.NotImplementedException();
        }
    }
}