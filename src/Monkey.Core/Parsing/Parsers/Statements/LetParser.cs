using Monkey.Core.Lexing;
using Monkey.Core.Parsing.Expressions;
using Monkey.Core.Parsing.Statements;

namespace Monkey.Core.Parsing.Parsers.Statements
{
    public class LetParser : StatementParser
    {
        public LetParser(Script2<Token> tokens) 
            : base(tokens)
        {
        }

        public override Statement ParseStatement(Token token)
        {
            if (token.TokenType != TokenType.LET)
                return Next?.ParseStatement(token);

            var identifierToken = Tokens.Next();
            Tokens.Next(); // =
            Tokens.Next(); // value
            Tokens.Next(); // semi colon
            return new LetStatement(token)
            {
                Name = Identifier.Create(identifierToken.Value)
            };
        }
    }
}