using System.Collections.Generic;
using Monkey.Core.Lexing;
using Monkey.Core.Parsing.Expressions;
using Monkey.Core.Parsing.Parsers.Expressions;
using Monkey.Core.Parsing.Statements;

namespace Monkey.Core.Parsing.Parsers.Statements
{
    public class ExpressionStatementParser : StatementParser
    {
        private readonly Dictionary<TokenType, ExpressionPriority> _precedences = new Dictionary<TokenType, ExpressionPriority>
        {
            { TokenType.EQ, ExpressionPriority.EQUALS},
            { TokenType.NOT_EQ, ExpressionPriority.EQUALS},
            { TokenType.LT, ExpressionPriority.LESS_GREATER},
            { TokenType.GT, ExpressionPriority.LESS_GREATER},
            { TokenType.PLUS, ExpressionPriority.SUM},
            { TokenType.MINUS, ExpressionPriority.SUM},
            { TokenType.SLASH, ExpressionPriority.PRODUCT},
            { TokenType.ASTERISK, ExpressionPriority.PRODUCT},
        };
        
        public ExpressionStatementParser(Script2<Token> tokens) 
            : base(tokens)
        {
        }

        public override Statement ParseStatement(Token token)
        {
            var expressionStatement = new ExpressionStatement(token)
            {
                Expression = ParseExpression(token, ExpressionPriority.LOWEST)
            };

            Tokens.Next(); // semi solon
            return expressionStatement;
        }

        private Expression ParseExpression(Token token, ExpressionPriority priority)
        {
            var leftExpression = Parse(token, priority);

            for (; Tokens.Peek().TokenType != TokenType.SEMICOLON && priority < PeekPrecedence();)
            {
                Tokens.Next();
                leftExpression = Parse(Tokens.Current(), CurrentPrecedence(), leftExpression);
            }
            return leftExpression;
        }

        private Expression Parse(Token token, ExpressionPriority priority)
        {
            var expression = ExpressionParser();
            return expression.ParseExpression(token, priority);
        }

        private Expression Parse(Token token, ExpressionPriority priority, Expression leftExpression)
        {
            var expression = ExpressionParser()
                .SetNext(new InfixParser(Tokens, Parse, leftExpression));
            return expression.ParseExpression(token, priority);
        }

        private ExpressionParser ExpressionParser() =>
            new IdentifierParser(Tokens)
                .SetNext(new IntegerParser(Tokens)
                    .SetNext(new BooleanParser(Tokens)
                        .SetNext(new ParenParser(Tokens, Parse)
                            .SetNext(new PrefixParser(Tokens, Parse)))));

        private ExpressionPriority PeekPrecedence()
        {
            var token = Tokens.Peek();
            return _precedences.ContainsKey(token.TokenType)
                ? _precedences[token.TokenType]
                : ExpressionPriority.LOWEST;
        }

        private ExpressionPriority CurrentPrecedence()
        {
            var token = Tokens.Current();
            return _precedences.ContainsKey(token.TokenType)
                ? _precedences[token.TokenType]
                : ExpressionPriority.LOWEST;
        }
    }
}