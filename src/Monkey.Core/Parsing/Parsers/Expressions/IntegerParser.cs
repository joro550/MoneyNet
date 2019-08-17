using Monkey.Core.Lexing;
using Monkey.Core.Parsing.Expressions;

namespace Monkey.Core.Parsing.Parsers.Expressions
{
    public class IntegerParser : ExpressionParser
    {
        public IntegerParser(Script2<Token> tokens) : base(tokens)
        {
        }

        public override Expression ParseExpression(Token token, ExpressionPriority lowest) =>
            token.TokenType != TokenType.INT
                ? Next?.ParseExpression(token, lowest)
                : Integer.Create(int.Parse(token.Value));
    }
}