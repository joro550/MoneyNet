using Monkey.Core.Lexing;
using Monkey.Core.Parsing.Expressions;

namespace Monkey.Core.Parsing.Statements
{
    public class ExpressionStatement : Statement
    {
        private Token Token { get; }
        public Expression Expression { get; set; }

        public ExpressionStatement(Token token)
        {
            Token = token;
        }
    }
}