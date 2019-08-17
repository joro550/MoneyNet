using Monkey.Core.Parsing;
using Monkey.Core.Parsing.Expressions;
using Monkey.Core.Parsing.Statements;
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
        
        [Fact]
        public void GivenAReturnStatement_ThenStatementsAreValid()
        {
            const string script = @"return 5; return 10; return 993322;";
            var syntaxTree = Parser.ParseScript(script);
            Assert.NotNull(syntaxTree);
            Assert.Equal(3, syntaxTree.Statements.Count);

            Assert.IsType<ReturnStatement>(syntaxTree.Statements[0]);
            Assert.IsType<ReturnStatement>(syntaxTree.Statements[1]);
            Assert.IsType<ReturnStatement>(syntaxTree.Statements[2]);
        }
        
        [Fact]
        public void GivenAnExpressionStatement_ThenStatementsAreValid()
        {
            const string script = @"foobar;";
            var syntaxTree = Parser.ParseScript(script);
            Assert.NotNull(syntaxTree);
            var statement = Assert.Single(syntaxTree.Statements);
            var expression = Assert.IsType<ExpressionStatement>(statement);
            var identifier = Assert.IsType<Identifier>(expression.Expression);
            Assert.Equal("foobar", identifier.Value);
        }
    }
}