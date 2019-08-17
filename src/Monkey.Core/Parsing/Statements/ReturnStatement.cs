using Monkey.Core.Lexing;
using Monkey.Core.Parsing.Expressions;

namespace Monkey.Core.Parsing.Statements
{
    public class ReturnStatement : Statement
    {
        public ReturnStatement(Token token)
        {
            Token = token;
        }

        private Token Token { get; }
        public Expression ReturnValue { get; set; }
    }
}