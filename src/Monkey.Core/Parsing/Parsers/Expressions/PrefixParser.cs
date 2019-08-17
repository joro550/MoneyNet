using Monkey.Core.Lexing;
using Monkey.Core.Parsing.Expressions;
using Monkey.Core.Parsing.Parsers.Statements;

namespace Monkey.Core.Parsing.Parsers.Expressions
{
    public class PrefixParser : ExpressionParser
    {
        private readonly Parse _parse;

        public PrefixParser(Script2<Token> tokens, Parse parse) 
            : base(tokens)
        {
            _parse = parse;
        }

        public override Expression ParseExpression(Token token, ExpressionPriority lowest)
        {
            return token.TokenType == TokenType.BANG || token.TokenType == TokenType.MINUS
                ? CreatePrefix(token)
                : Next?.ParseExpression(token, lowest);
        }

        private Prefix CreatePrefix(Token token)
        {
            var nextToken = Tokens.Next();
            return Prefix.Create(token.Value, _parse(nextToken, ExpressionPriority.PREFIX));
        }
    }
}