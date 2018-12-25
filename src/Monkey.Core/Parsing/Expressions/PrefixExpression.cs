using Monkey.Core.Lexing;

namespace Monkey.Core.Parsing.Expressions
{
    public class PrefixExpression : IExpression
    {
        public Token Token { get; }
        public string Operator { get; private set; }
        public IExpression Right { get; private set; }

        private PrefixExpression(Token token) 
            => Token = token;

        public string TokenLiteral()
        {
            return string.Empty;
        }

        public static IExpression Parse(ExpressionFactory factory, Parser parser)
        {
            var currentToken = parser.CurrentToken();
            parser.NextToken();
            return new PrefixExpression(currentToken)
            {
                Right = factory.GetPrefixExpression(parser, Priority.Prefix),
                Operator = currentToken.Value
            };
        }
    }
}