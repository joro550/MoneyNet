using Monkey.Core.Parsing;
using Xunit;

namespace Monkey.Core.Tests.Unit.Parsing
{
    public class ParserTests
    {
        [Fact]
        public void WhenParsingEmptyScript_ThenEmptyTreeIsReturned()
        {
            var script = string.Empty;
            var syntaxTree = Parser.ParseScript(script);

            Assert.IsType<SyntaxTree>(syntaxTree);
            Assert.Empty(syntaxTree.Statements);
        }

        [Fact]
        public void GivenALetStatement_ThenStatementsAreValid()
        {
            const string script = @"let x= 5; let y = 10; let foobar = 838383;";
            var syntaxTree = Parser.ParseScript(script);
            Assert.NotNull(syntaxTree);
            Assert.Equal(3, syntaxTree.Statements.Count);

            var firstStatement = Assert.IsType<LetStatement>(syntaxTree.Statements[0]);
            var secondStatement = Assert.IsType<LetStatement>(syntaxTree.Statements[1]);
            var thirdStatement = Assert.IsType<LetStatement>(syntaxTree.Statements[2]);
            
            Assert.Equal("x", firstStatement.Name.Value);
            Assert.Equal("y", secondStatement.Name.Value);
            Assert.Equal("foobar", thirdStatement.Name.Value);

        }
    }
}