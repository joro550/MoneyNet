using Monkey.Core.Lexing;
using Monkey.Core.Parsing.Expressions;

namespace Monkey.Core.Parsing.Parsers.Expressions
{
    public class IdentifierParser : ExpressionParser
    {
        public IdentifierParser(Script2<Token> tokens) : base(tokens)
        {
        }

        public override Expression ParseExpression(Token token, ExpressionPriority lowest) =>
            token.TokenType != TokenType.IDENT
                ? Next?.ParseExpression(token, lowest)
                : Identifier.CreateIdentifier(token.Value);
    }
}