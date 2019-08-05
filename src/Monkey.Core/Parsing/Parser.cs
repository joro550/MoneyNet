using System.Collections.Generic;
using Monkey.Core.Lexing;

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
                var parsers = new LetParser(tokens);
                statements.Add(parsers.ParseStatement(token));
            }
            return new SyntaxTree {Statements = statements};
        }
    }

    public abstract class StatementParser
    {
        protected Script2<Token> Tokens { get; }
        protected StatementParser Next { get; private set; }

        protected StatementParser(Script2<Token> tokens)
        {
            Tokens = tokens;
        }

        public StatementParser SetNext(StatementParser statementParser)
        {
            Next = statementParser;
            return this;
        }

        public abstract Statement ParseStatement(Token token);
    }

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
                Name = Identifier.CreateIdentifier(identifierToken.Value)
            };
        }
    }

    public class SyntaxTree
    {
        public List<Statement> Statements = new List<Statement>();
        
    }

    public class Identifier
    {
        public TokenType Token { get; }
        public string Value { get; }

        private Identifier(TokenType token, string value)
        {
            Token = token;
            Value = value;
        }

        public static Identifier CreateIdentifier(string value) 
            => new Identifier(TokenType.IDENT, value);
    }

    public abstract class Statement
    {
        
    }

    public abstract class Expression
    {
        
    }

    public class LetStatement : Statement
    {
        private Token Token { get; }
        public Identifier Name { get; set; } 
        public Expression Value { get; set; }

        public LetStatement(Token token)
        {
            Token = token;
        }
    }
}