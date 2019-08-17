using Monkey.Core.Lexing;
using Monkey.Core.Parsing.Expressions;

namespace Monkey.Core.Parsing.Parsers.Expressions
{
    public class BooleanParser : ExpressionParser
    {
        public BooleanParser(Script2<Token> tokens) : base(tokens)
        {
        }

        public override Expression ParseExpression(Token token, ExpressionPriority lowest) =>
            token.TokenType != TokenType.TRUE && token.TokenType != TokenType.FALSE
                ? Next?.ParseExpression(token, lowest)
                : Boolean.Create(bool.Parse(token.Value));
    }
}