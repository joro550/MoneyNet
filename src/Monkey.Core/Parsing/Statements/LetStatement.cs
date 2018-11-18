using Monkey.Core.Lexing;

namespace Monkey.Core.Parsing.Statements
{
    public class LetStatement : IStatement
    {
        public LetStatement(Token token)
        {
            Token = token;
        }

        public Token Token { get; }
        public string Name { get; set; }
        public IExpression Value { get; set; }

        public string TokenLiteral()
        {
            return Token.Value;
        }
    }
}