using Monkey.Core.Lexing;
using Monkey.Core.Parsing.Expressions;

namespace Monkey.Core.Parsing.Statements
{
    public class ExpressionStatement : IStatement
    {
        public ExpressionStatement(Token token)
        {
            Token = token;
        }

        public Token Token { get; }
        public IExpression Expression { get; set; }

        public string TokenLiteral()
            => Token.Value;
    }
}