using System.Collections.Generic;
using Monkey.Core.Lexing;
using Monkey.Core.Parsing.Parsers.Statements;
using Monkey.Core.Parsing.Statements;

namespace Monkey.Core.Parsing
{
    public static class Parser
    {
        public static SyntaxTree ParseScript(string script)
        {
            var tokens = new Script2<Token>(new Lexer()
                .ParseScript(script));

            var statements = new List<Statement>();
            for (var token = tokens.Current(); token.TokenType != TokenType.EOF; token = tokens.Next())
            {
                var parsers = new LetParser(tokens)
                    .SetNext(new ReturnParser(tokens)
                        .SetNext(new ExpressionStatementParser(tokens)));
                statements.Add(parsers.ParseStatement(token));
            }
            return new SyntaxTree {Statements = statements};
        }
    }
}