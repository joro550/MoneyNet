using Monkey.Core.Lexing;

namespace Monkey.Core.Parsing
{
    public class LetStatement : Statement
    {
        private LetStatement(Token token) 
            : base(token)
        {
        }

        public static IStatement CreateFromTokens(TokenCollection tokens)
        {
            var statement = new LetStatement(tokens.Current());

            if (ExpectPeek(tokens, TokenType.IDENT))
            {
                var identityToken = tokens.Next();
                statement.Name.Token = identityToken;
                statement.Name.Value = identityToken.Value;
            }
            else
                statement.Errors.Add($"Expected next token to be {TokenType.IDENT.ToString()}");

            if (ExpectPeek(tokens, TokenType.ASSIGN))
                tokens.Next();
            else
                statement.Errors.Add($"Expected next token to be {TokenType.ASSIGN.ToString()}");


            //TODO: we're skipping expressions for now
            while (tokens.Current().TokenType != TokenType.SEMICOLON)
                tokens.Next();
            return statement;
        }

        private static bool ExpectPeek(TokenCollection tokens, TokenType tokenType) 
            => tokens.Peek().TokenType == tokenType;
    }
}