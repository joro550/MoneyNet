using Monkey.Core.Lexing;

namespace Monkey.Core.Parsing.Expressions
{
    public class Prefix : Expression
    {
        public TokenType Token { get; }
        public string Operator { get; }
        public Expression Right { get; }

        private Prefix(TokenType token, string value, Expression right)
        {
            Token = token;
            Operator = value;
            Right = right;
        }

        public static Prefix Create(string value, Expression right) 
            => new Prefix(TokenType.IDENT, value, right);
    }
}