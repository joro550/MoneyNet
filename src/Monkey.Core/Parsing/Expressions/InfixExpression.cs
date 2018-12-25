using Monkey.Core.Lexing;
using System.Collections.Generic;

namespace Monkey.Core.Parsing.Expressions
{
    public class InfixExpression : IExpression
    {
        public Token Token { get; }
        public IExpression Left { get; set; }
        public IExpression Right { get; set; }
        public string Operator { get; set; }

        private readonly Dictionary<TokenType, Priority> _precedence = new Dictionary<TokenType, Priority>
        {
            { TokenType.EQ, Priority.Equals },
            { TokenType.NOT_EQ, Priority.Equals },
            { TokenType.LT, Priority.LessGreater },
            { TokenType.GT, Priority.LessGreater },
            { TokenType.PLUS, Priority.Sum },
            { TokenType.MINUS, Priority.Sum },
            { TokenType.SLASH, Priority.Product },
            { TokenType.ASTERISK, Priority.Product }
        };

        private InfixExpression(Token token) 
            => Token = token;

        private Priority GetPriority(Token token) => _precedence.ContainsKey(token.TokenType) 
            ? _precedence[token.TokenType] 
            : Priority.Lowest;


        public string TokenLiteral()
            => Token.Value;

        public static IExpression Parse(IExpression left, ExpressionFactory factory, Parser parser)
        {
            var currentToken = parser.CurrentToken();
            var infix = new InfixExpression(currentToken)
            {
                Left = left,
                Operator = currentToken.Value
            };

            var priority = infix.GetPriority(currentToken);
            parser.NextToken();
            infix.Right = factory.GetPrefixExpression(parser, priority);
            return infix;
        }
    }
}