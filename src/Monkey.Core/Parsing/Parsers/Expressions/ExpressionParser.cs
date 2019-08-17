using Monkey.Core.Lexing;
using Monkey.Core.Parsing.Expressions;

namespace Monkey.Core.Parsing.Parsers.Expressions
{
    public abstract class ExpressionParser
    {
        protected Script2<Token> Tokens { get; }
        protected ExpressionParser Next { get; private set; }

        protected ExpressionParser(Script2<Token> tokens)
        {
            Tokens = tokens;
        }

        public ExpressionParser SetNext(ExpressionParser statementParser)
        {
            Next = statementParser;
            return this;
        }

        public abstract Expression ParseExpression(Token token, ExpressionPriority lowest);
    }
}