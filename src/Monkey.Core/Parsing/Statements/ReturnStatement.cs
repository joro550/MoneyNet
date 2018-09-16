using Monkey.Core.Lexing;

namespace Monkey.Core.Parsing
{
    public class ReturnStatement : Statement
    {
        private ReturnStatement(Token token) 
            : base(token)
        {
        }

        public static IStatement CreateFromTokens(TokenCollection tokens)
        {
            var statement = new ReturnStatement(tokens.Current());
            
            //TODO: we're skipping expressions for now
            while (tokens.Current().TokenType != TokenType.SEMICOLON)
                tokens.Next();
            return statement;
        }
    }
}