using System.Collections.Generic;
using System.Linq;
using Monkey.Core.Lexing;
using Monkey.Core.Parsing;
using Xunit;

namespace Monkey.Core.Tests.Unit.Parsing
{
    public class ReturnStatementParsingTests
    {
        [Fact]
        public void WhenTokensArePassedToParse_ThenExpectedProgramIsReturned()
        {
            var tokens = new List<Token>
            {
                new Token(TokenType.RETURN, "return"),
                new Token(TokenType.INT, "5"),
                new Token(TokenType.SEMICOLON, ";"),

                new Token(TokenType.RETURN, "return"),
                new Token(TokenType.INT, "10"),
                new Token(TokenType.SEMICOLON, ";"),

                new Token(TokenType.RETURN, "return"),
                new Token(TokenType.INT, "993322"),
                new Token(TokenType.SEMICOLON, ";"),

                new Token(TokenType.EOF, "")
            };
            
            var parser = new Parser();
            var actualProgram = parser.ParseTokens(tokens);

            var statements = actualProgram.GetStatements().OfType<ReturnStatement>().ToList();
            Assert.Equal(3, statements.Count);
            
            Assert.Equal(TokenType.RETURN, statements[0].Token.TokenType);
            Assert.Equal(TokenType.RETURN, statements[1].Token.TokenType);
            Assert.Equal(TokenType.RETURN, statements[2].Token.TokenType);
        }
    }
}