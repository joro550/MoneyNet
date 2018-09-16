using System.Collections.Generic;
using System.Linq;
using Monkey.Core.Lexing;
using Xunit;

namespace Monkey.Core.Tests.Unit.Lexing
{
    public class LexerTests
    {
        [Theory]
        [InlineData("let", TokenType.LET)]
        [InlineData("5", TokenType.INT)]
        [InlineData("=", TokenType.ASSIGN)]
        [InlineData("+", TokenType.PLUS)]
        [InlineData("-", TokenType.MINUS)]
        [InlineData("!", TokenType.BANG)]
        [InlineData("*", TokenType.ASTERISK)]
        [InlineData("/", TokenType.SLASH)]
        [InlineData("<", TokenType.LT)]
        [InlineData(">", TokenType.GT)]
        [InlineData("==", TokenType.EQ)]
        [InlineData("!=", TokenType.NOT_EQ)]
        [InlineData(",", TokenType.COMMA)]
        [InlineData(";", TokenType.SEMICOLON)]
        [InlineData("(", TokenType.LeftParen)]
        [InlineData(")", TokenType.RightParen)]
        [InlineData("{", TokenType.LeftBrace)]
        [InlineData("}", TokenType.RightBrace)]
        [InlineData("fn", TokenType.FUNCTION)]
        [InlineData("true", TokenType.TRUE)]
        [InlineData("false", TokenType.FALSE)]
        [InlineData("if", TokenType.IF)]
        [InlineData("else", TokenType.ELSE)]
        [InlineData("return", TokenType.RETURN)]
        [InlineData("myName", TokenType.IDENT)]
        public void WhenSingleWordIsPassedIn_RepresentativeTokenIsReturned(string script, TokenType expectedTokenType)
        {
            var lexer = new Lexer();
            var expectedToken = new Token(expectedTokenType, script);
            var actualToken = lexer.ParseScript(script);

            Assert.Equal(1, actualToken.Count);
            Assert.Equal(expectedToken.TokenType, actualToken[0].TokenType);
            Assert.Equal(expectedToken.Value, actualToken[0].Value);
        }

        [Theory]
        [InlineData("let number = 5", TokenType.LET, TokenType.IDENT, TokenType.ASSIGN, TokenType.INT)]
        [InlineData("let add = fn(x, y) {}", TokenType.LET, TokenType.IDENT, 
            TokenType.ASSIGN, TokenType.FUNCTION, TokenType.LeftParen, TokenType.IDENT, 
            TokenType.COMMA, TokenType.IDENT, TokenType.RightParen, TokenType.LeftBrace, TokenType.RightBrace)]
        
        public void WhenALineOfScriptIsProvided_ThenTokensAreAsExpected(string script,
            params TokenType[] expectedTokens)
        {
            var lexer = new Lexer();
            var actualToken = lexer.ParseScript(script);

            Assert.Equal(expectedTokens.Length, actualToken.Count);
            Assert.Equal(expectedTokens, actualToken.Select(token => token.TokenType));
        }

        [Fact]
        public void WhenAScriptIsProvided_ExpectedTokensAreReturned()
        {
            const string script = @"let five = 5;
                let ten = 10;
                let add = fn(x, y) {
                    x + y;
                };
    
                let result = add(five, ten);
                !-/*5;
                5 < 10 > 5;
                if (5 < 10) {
                    return true;
                } else {
                    return false;
                }
                10 == 10;
                10 != 9;";

            var expectedTokens = new List<Token>
            {
                new Token(TokenType.LET, "let"),
                new Token(TokenType.IDENT, "five"),
                new Token(TokenType.ASSIGN, "="),
                new Token(TokenType.INT, "5"),
                new Token(TokenType.SEMICOLON, ";"),
                new Token(TokenType.LET, "let"),
                new Token(TokenType.IDENT, "ten"),
                new Token(TokenType.ASSIGN, "="),
                new Token(TokenType.INT, "10"),
                new Token(TokenType.SEMICOLON, ";"),
                new Token(TokenType.LET, "let"),
                new Token(TokenType.IDENT, "add"),
                new Token(TokenType.ASSIGN, "="),
                new Token(TokenType.FUNCTION, "fn"),
                new Token(TokenType.LeftParen, "("),
                new Token(TokenType.IDENT, "x"),
                new Token(TokenType.COMMA, ","),
                new Token(TokenType.IDENT, "y"),
                new Token(TokenType.RightParen, ")"),
                new Token(TokenType.LeftBrace, "{"),
                new Token(TokenType.IDENT, "x"),
                new Token(TokenType.PLUS, "+"),
                new Token(TokenType.IDENT, "y"),
                new Token(TokenType.SEMICOLON, ";"),
                new Token(TokenType.RightBrace, "}"),
                new Token(TokenType.SEMICOLON, ";"),
                new Token(TokenType.LET, "let"),
                new Token(TokenType.IDENT, "result"),
                new Token(TokenType.ASSIGN, "="),
                new Token(TokenType.IDENT, "add"),
                new Token(TokenType.LeftParen, "("),
                new Token(TokenType.IDENT, "five"),
                new Token(TokenType.COMMA, ","),
                new Token(TokenType.IDENT, "ten"),
                new Token(TokenType.RightParen, ")"),
                new Token(TokenType.SEMICOLON, ";"),
                new Token(TokenType.BANG, "!"),
                new Token(TokenType.MINUS, "-"),
                new Token(TokenType.SLASH, "/"),
                new Token(TokenType.ASTERISK, "*"),
                new Token(TokenType.INT, "5"),
                new Token(TokenType.SEMICOLON, ";"),
                new Token(TokenType.INT, "5"),
                new Token(TokenType.LT, "<"),
                new Token(TokenType.INT, "10"),
                new Token(TokenType.GT, ">"),
                new Token(TokenType.INT, "5"),
                new Token(TokenType.SEMICOLON, ";"),
                new Token(TokenType.IF, "if"),
                new Token(TokenType.LeftParen, "("),
                new Token(TokenType.INT, "5"),
                new Token(TokenType.LT, "<"),
                new Token(TokenType.INT, "10"),
                new Token(TokenType.RightParen, ")"),
                new Token(TokenType.LeftBrace, "{"),
                new Token(TokenType.RETURN, "return"),
                new Token(TokenType.TRUE, "true"),
                new Token(TokenType.SEMICOLON, ";"),
                new Token(TokenType.RightBrace, "}"),
                new Token(TokenType.ELSE, "else"),
                new Token(TokenType.LeftBrace, "{"),
                new Token(TokenType.RETURN, "return"),
                new Token(TokenType.FALSE, "false"),
                new Token(TokenType.SEMICOLON, ";"),
                new Token(TokenType.RightBrace, "}"),
                new Token(TokenType.INT, "10"),
                new Token(TokenType.EQ, "=="),
                new Token(TokenType.INT, "10"),
                new Token(TokenType.SEMICOLON, ";"),
                new Token(TokenType.INT, "10"),
                new Token(TokenType.NOT_EQ, "!="),
                new Token(TokenType.INT, "9"),
                new Token(TokenType.SEMICOLON, ";"),
                new Token(TokenType.EOF, "")
            };

            var lexer = new Lexer();
            var actualTokens = lexer.ParseScript(script);
            Assert.Equal(expectedTokens.Count, actualTokens.Count);
            for (var index = 0; index < expectedTokens.Count; index++)
            {
                Assert.Equal(expectedTokens[index].TokenType, actualTokens[index].TokenType);
                Assert.Equal(expectedTokens[index].Value, actualTokens[index].Value);
            }
        }
    }
}