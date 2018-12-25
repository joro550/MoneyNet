using Monkey.Core.Lexing;
using Monkey.Core.Parsing.Statements;

namespace Monkey.Core.Parsing.Expressions
{
    public class IntegerExpression : IExpression
    {
        private readonly Token _currentToken;
        public int Value { get; private set; }

        private IntegerExpression(Token currentToken) 
            => _currentToken = currentToken;

        public string TokenLiteral() 
            => _currentToken.Value;

        public static IExpression Parse(ExpressionFactory factory, Parser parser)
        {
            var currentToken = parser.CurrentToken();
            var expression = new IntegerExpression(currentToken);
            if (int.TryParse(currentToken.Value, out var value))
                expression.Value = value;
            
            return expression;
        }
    }
}