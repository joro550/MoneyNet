using Monkey.Core.Lexing;

namespace Monkey.Core.Parsing.Expressions
{
    public class Integer : Expression
    {
        public TokenType Token { get; }
        public int Value { get; }

        private Integer(TokenType token, int value)
        {
            Token = token;
            Value = value;
        }

        public static Integer Create(int value) 
            => new Integer(TokenType.IDENT, value);
    }
}