using Monkey.Core.Lexing;

namespace Monkey.Core.Parsing.Expressions
{
    public class Infix : Expression
    {
        public TokenType Token { get; }
        public string Operator { get; }
        public Expression Right { get; }
        public Expression Left { get; }

        private Infix(TokenType token, string value, Expression left, Expression right)
        {
            Token = token;
            Operator = value;
            Right = right;
            Left = left;
        }

        public static Infix Create(string value, Expression left, Expression right) 
            => new Infix(TokenType.IDENT, value, left, right);
    }
}