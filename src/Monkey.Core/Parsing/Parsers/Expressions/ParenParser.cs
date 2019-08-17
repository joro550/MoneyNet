using Monkey.Core.Lexing;
using Monkey.Core.Parsing.Expressions;

namespace Monkey.Core.Parsing.Parsers.Expressions
{
    public class ParenParser : ExpressionParser
    {
        private readonly Parse _parse;

        public ParenParser(Script2<Token> tokens, Parse parse) 
            : base(tokens)
        {
            _parse = parse;
        }

        public override Expression ParseExpression(Token token, ExpressionPriority lowest)
        {
            if (token.TokenType != TokenType.LeftParen)
                return Next?.ParseExpression(token, lowest);

            var nextToken = Tokens.Next();
            var expression = _parse(nextToken, ExpressionPriority.LOWEST);
            Tokens.Next(); // Right Paren
            return expression;
        }
    }
}