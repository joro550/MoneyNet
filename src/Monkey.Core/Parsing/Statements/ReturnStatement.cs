using Monkey.Core.Lexing;

namespace Monkey.Core.Parsing.Statements
{
    public class ReturnStatement : IStatement
    {
        public ReturnStatement(Token token) 
            => Token = token;


        public Token Token { get; }
        public IExpression ReturnValue { get; set; }
        
        public string TokenLiteral() 
            => Token.Value;
    }
}