using Monkey.Core.Lexing;
using Monkey.Core.Parsing.Expressions;

namespace Monkey.Core.Parsing.Statements
{
    public class LetStatement : Statement
    {
        private Token Token { get; }
        public Identifier Name { get; set; } 
        public Expression Value { get; set; }

        public LetStatement(Token token)
        {
            Token = token;
        }
    }
}