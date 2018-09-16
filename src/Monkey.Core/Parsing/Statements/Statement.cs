using System.Collections.Generic;
using Monkey.Core.Lexing;

namespace Monkey.Core.Parsing
{
    public class Statement : IStatement
    {
        public Token Token { get; }
        public Identifier Name { get; }
        public IExpressioin Value { get; protected set; }
        public List<string> Errors { get; }

        protected Statement(Token token)
        {
            Token = token;
            Name = new Identifier();
            Errors = new List<string>();
        }

        public virtual void StatementNode()
        {
        }

        public virtual string TokenLiteral() 
            => Token.TokenType.ToString();
    }
}