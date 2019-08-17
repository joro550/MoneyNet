using Monkey.Core.Lexing;
using Monkey.Core.Parsing.Statements;

namespace Monkey.Core.Parsing.Parsers.Statements
{
    public class ReturnParser : StatementParser
    {
        public ReturnParser(Script2<Token> tokens) 
            : base(tokens)
        {
        }

        public override Statement ParseStatement(Token token)
        {
            if (token.TokenType != TokenType.RETURN)
                return Next?.ParseStatement(token);

            Tokens.Next(); // value
            Tokens.Next(); // semi colon
            return new ReturnStatement(token);
        }
    }
}