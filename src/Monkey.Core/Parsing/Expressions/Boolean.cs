using Monkey.Core.Lexing;

namespace Monkey.Core.Parsing.Expressions
{
    public class Boolean : Expression
    {
        public TokenType Token { get; }
        public bool Value { get; }

        private Boolean(TokenType token, bool value)
        {
            Token = token;
            Value = value;
        }

        public static Boolean Create(bool value) 
            => new Boolean(TokenType.IDENT, value);
    }
}