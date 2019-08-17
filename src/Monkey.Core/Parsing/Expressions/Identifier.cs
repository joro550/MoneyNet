using Monkey.Core.Lexing;

namespace Monkey.Core.Parsing.Expressions
{
    public class Identifier : Expression
    {
        public TokenType Token { get; }
        public string Value { get; }

        private Identifier(TokenType token, string value)
        {
            Token = token;
            Value = value;
        }

        public static Identifier Create(string value) 
            => new Identifier(TokenType.IDENT, value);
    }
}