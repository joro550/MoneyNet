using System.Collections.Generic;
using System.Linq;
using Monkey.Core.Lexing;
using Monkey.Core.Parsing;
using Xunit;

namespace Monkey.Core.Tests.Unit.Parsing
{
    public class LetStatementParsingTests
    {
        [Fact]
        public void WhenTokensArePassedToParse_ThenExpectedProgramIsReturned()
        {
            var tokens = new List<Token>
            {
                new Token(TokenType.LET, "let"),
                new Token(TokenType.IDENT, "x"),
                new Token(TokenType.ASSIGN, "="),
                new Token(TokenType.INT, "5"),
                new Token(TokenType.SEMICOLON, ";"),
                
                new Token(TokenType.LET, "let"),
                new Token(TokenType.IDENT, "y"),
                new Token(TokenType.ASSIGN, "="),
                new Token(TokenType.INT, "10"),
                new Token(TokenType.SEMICOLON, ";"),

                new Token(TokenType.LET, "let"),
                new Token(TokenType.IDENT, "foobar"),
                new Token(TokenType.ASSIGN, "="),
                new Token(TokenType.INT, "838383"),
                new Token(TokenType.SEMICOLON, ";"),
                
                new Token(TokenType.EOF, "")
            };
            
            var parser = new Parser();
            var actualProgram = parser.ParseTokens(tokens);

            var statements = actualProgram.GetStatements().OfType<LetStatement>().ToList();
            Assert.Equal(3, statements.Count);
            
            Assert.Equal("x", statements[0].Name.Value);
            Assert.Equal("y", statements[1].Name.Value);
            Assert.Equal("foobar", statements[2].Name.Value);
        }
        
        [Fact]
        public void WhenAssignIsNotPreset_ThenAssignErrorIsAddedToErrorArray()
        {
            var tokens = new List<Token>
            {
                new Token(TokenType.LET, "let"),
                new Token(TokenType.IDENT, "x"),
                new Token(TokenType.INT, "5"),
                new Token(TokenType.SEMICOLON, ";"),
                new Token(TokenType.EOF, "")
            };
            
            var parser = new Parser();
            var actualProgram = parser.ParseTokens(tokens);

            Assert.Equal(1, actualProgram.Errors().Count);
            Assert.Equal("expected next token to be IDENT", actualProgram.Errors()[0]);
        }
        
        [Fact]
        public void WhenIdentifierIsNotPreset_ThenAssignErrorIsAddedToErrorArray()
        {
            var tokens = new List<Token>
            {
                new Token(TokenType.LET, "let"),
                new Token(TokenType.ASSIGN, "="),
                new Token(TokenType.INT, "5"),
                new Token(TokenType.SEMICOLON, ";"),
                new Token(TokenType.EOF, "")
            };
            
            var parser = new Parser();
            var actualProgram = parser.ParseTokens(tokens);

            Assert.Equal(1, actualProgram.Errors().Count);
            Assert.Equal("expected next token to be IDENT", actualProgram.Errors()[0]);
        }
    }
}