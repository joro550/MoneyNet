using Monkey.Core.Lexing;
using Monkey.Core.Parsing.Statements;

namespace Monkey.Core.Parsing.Parsers.Statements
{
    public abstract class StatementParser
    {
        protected Script2<Token> Tokens { get; }
        protected StatementParser Next { get; private set; }

        protected StatementParser(Script2<Token> tokens)
        {
            Tokens = tokens;
        }

        public StatementParser SetNext(StatementParser statementParser)
        {
            Next = statementParser;
            return this;
        }

        public abstract Statement ParseStatement(Token token);
    }
}