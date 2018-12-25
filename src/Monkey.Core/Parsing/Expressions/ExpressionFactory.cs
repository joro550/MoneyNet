using System.Collections.Generic;
using Monkey.Core.Lexing;
using Monkey.Core.Parsing.Expressions;
using Monkey.Core.Parsing.Statements;

namespace Monkey.Core.Parsing
{
    public class ExpressionFactory
    {
        private delegate IExpression ParseExpressionPrefix(ExpressionFactory factory, Parser parser);
        private delegate IExpression ParseExpressionInfix(IExpression left, ExpressionFactory factory, Parser parser);
        
        private readonly Dictionary<TokenType, ParseExpressionPrefix> _parsePrefixes
            = new Dictionary<TokenType, ParseExpressionPrefix>
            {
                {TokenType.IDENT, IdentifierExpression.Parse},
                {TokenType.INT, IntegerExpression.Parse},
                {TokenType.MINUS, PrefixExpression.Parse},
                {TokenType.BANG, PrefixExpression.Parse}
            };
        
        private readonly Dictionary<TokenType, ParseExpressionInfix> _parseInfix 
            = new Dictionary<TokenType, ParseExpressionInfix>
            {
                {TokenType.PLUS, InfixExpression.Parse},
                {TokenType.MINUS, InfixExpression.Parse},
                {TokenType.SLASH, InfixExpression.Parse},
                {TokenType.ASTERISK, InfixExpression.Parse},
                {TokenType.EQ, InfixExpression.Parse},
                {TokenType.NOT_EQ, InfixExpression.Parse},
                {TokenType.LT, InfixExpression.Parse},
                {TokenType.GT, InfixExpression.Parse},
            };
        
        public ExpressionStatement GetExpression(Parser parser)
        {
            var currentToken = parser.CurrentToken();
            var expression = GetPrefixExpression(parser, Priority.Lowest);

            for (var newToken = parser.NextToken();
                newToken.TokenType != TokenType.SEMICOLON;
                newToken = parser.NextToken())
            {
                expression = GetInfixExpression(expression, parser);
            }

            return new ExpressionStatement(currentToken)
            {
                Expression = expression 
            };
        }

        public IExpression GetPrefixExpression(Parser parser, Priority priority)
        {
            var ct = parser.CurrentToken();
            return _parsePrefixes.ContainsKey(ct.TokenType) 
                ? _parsePrefixes[ct.TokenType](this, parser) 
                : null;
        }
        
        public IExpression GetInfixExpression(IExpression left, Parser parser)
        {
            var ct = parser.CurrentToken();
            return _parseInfix.ContainsKey(ct.TokenType) 
                ? _parseInfix[ct.TokenType](left, this, parser) 
                : null;
        }

    }
}