using Monkey.Core.Lexing;

namespace Monkey.Core.Parsing.Expressions
{
    public class If : Expression
    {
        public TokenType Token { get; }
        public Expression Condition { get; }
        public BlockStatement Consequence { get; }
        public BlockStatement Alternative { get; }
    }
}