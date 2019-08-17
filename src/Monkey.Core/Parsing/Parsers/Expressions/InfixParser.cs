using System.Collections.Generic;
using Monkey.Core.Lexing;
using Monkey.Core.Parsing.Expressions;

namespace Monkey.Core.Parsing.Parsers.Expressions
{
    public class InfixParser : ExpressionParser
    {
        private readonly Parse _parse;
        private readonly Expression _leftExpression;
        
        private readonly List<TokenType> _infixes = new List<TokenType>
        {
            TokenType.PLUS,
            TokenType.MINUS,
            TokenType.SLASH,
            TokenType.ASTERISK,
            TokenType.EQ,
            TokenType.NOT_EQ,
            TokenType.LT,
            TokenType.EQ,
        };

        public InfixParser(Script2<Token> tokens, Parse parse, Expression leftExpression) 
            : base(tokens)
        {
            _parse = parse;
            _leftExpression = leftExpression;
        }

        public override Expression ParseExpression(Token token, ExpressionPriority lowest)
        {
            return _infixes.Contains(token.TokenType)
                ? CreatePrefix(token, lowest)
                : Next?.ParseExpression(token, lowest);
        }

        private Infix CreatePrefix(Token token, ExpressionPriority lowest)
        {
            var nextToken = Tokens.Next();
            return Infix.Create(token.Value, _leftExpression, _parse(nextToken, lowest));
        }
    }
}