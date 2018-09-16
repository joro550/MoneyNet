using System.Collections.Generic;
using System.Linq;

namespace Monkey.Core.Lexing
{
    public class Lexer
    {
        private readonly Dictionary<string, TokenType> Keywords = new Dictionary<string, TokenType>
        {
            {"fn", TokenType.FUNCTION},
            {"let", TokenType.LET},
            {"true", TokenType.TRUE},
            {"false", TokenType.FALSE},
            {"if", TokenType.IF},
            {"else", TokenType.ELSE},
            {"return", TokenType.RETURN}
        };
        
        public IList<Token> ParseScript(string scriptText)
        {
            var script = new Script(scriptText);
            var tokens = new List<Token>();

            var currentToken = script.Current();
            while (currentToken != null)
            {
                if (IsEmptyOrWhiteSpace(currentToken))
                {
                    currentToken = script.Next();
                    continue;
                }

                tokens.Add(ParseToken(currentToken, script));

                currentToken = script.Next();
            }

            tokens.Add(Token(TokenType.EOF, ""));
            return tokens;
        }

        private static bool IsEmptyOrWhiteSpace(string value) =>
            value.All(char.IsWhiteSpace);

        private Token ParseToken(string currentToken, Script script)
        {
            switch (currentToken)
            {
                case "=":
                {
                    if (script.Peek() == '=')
                    {
                        script.Next();
                        return Token(TokenType.EQ, "==");
                    }

                    return Token(TokenType.ASSIGN, currentToken);
                }
                case "!":
                    if (script.Peek() == '=')
                    {
                        script.Next();
                        return Token(TokenType.NOT_EQ, "!=");
                    }
                    return Token(TokenType.BANG, currentToken);
                case "+":
                    return Token(TokenType.PLUS, currentToken);
                case "-":
                    return Token(TokenType.MINUS, currentToken);
                case "*":
                    return Token(TokenType.ASTERISK, currentToken);
                case "/":
                    return Token(TokenType.SLASH, currentToken);
                case "<":
                    return Token(TokenType.LT, currentToken);
                case ">":
                    return Token(TokenType.GT, currentToken);
                case ",":
                    return Token(TokenType.COMMA, currentToken);
                case ";":
                    return Token(TokenType.SEMICOLON, currentToken);
                case "(":
                    return Token(TokenType.LeftParen, currentToken);
                case ")":
                    return Token(TokenType.RightParen, currentToken);
                case "{":
                    return Token(TokenType.LeftBrace, currentToken);
                case "}":
                    return Token(TokenType.RightBrace, currentToken);
                default:
                {
                    if (currentToken.Length < 1)
                        return Token(TokenType.ILLEGAL, string.Empty);

                    var currentCharValue = currentToken[0];
                    var identifier = script.Current();

                    if (char.IsDigit(currentCharValue))
                    {
                        while (char.IsDigit(script.Peek()))
                            identifier += script.Next();
                        return Token(TokenType.INT, identifier);
                    }

                    if (!char.IsLetter(currentCharValue)) 
                        return Token(TokenType.ILLEGAL, string.Empty);

                    while (char.IsLetter(script.Peek()))
                        identifier += script.Next();

                    return Token(Keywords.ContainsKey(identifier) 
                        ? Keywords[identifier] 
                        : TokenType.IDENT, identifier);
                }
            }
        }

        private static Token Token(TokenType tokenType, string value) 
            => new Token(tokenType, value);

    }
}