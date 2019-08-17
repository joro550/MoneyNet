using Monkey.Core.Lexing;
using Monkey.Core.Parsing.Expressions;
using Monkey.Core.Parsing.Parsers.Expressions;
using Monkey.Core.Parsing.Statements;

namespace Monkey.Core.Parsing.Parsers.Statements
{
    public class ExpressionStatementParser : StatementParser
    {
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
            var expression = new IdentifierParser(Tokens);
            return expression.ParseExpression(token, priority);
        }
    }
}