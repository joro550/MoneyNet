using System.Linq;
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
        public void GivenAnIdentifierExpressionStatement_ThenStatementsAreValid()
        {
            const string script = @"foobar;";
            var syntaxTree = Parser.ParseScript(script);
            Assert.NotNull(syntaxTree);
            var statement = Assert.Single(syntaxTree.Statements);
            var expression = Assert.IsType<ExpressionStatement>(statement);
            var identifier = Assert.IsType<Identifier>(expression.Expression);
            Assert.Equal("foobar", identifier.Value);
        }
        
        [Fact]
        public void GivenAnIntegerExpressionStatement_ThenStatementsAreValid()
        {
            const string script = @"5;";
            var syntaxTree = Parser.ParseScript(script);
            Assert.NotNull(syntaxTree);
            var statement = Assert.Single(syntaxTree.Statements);
            var expression = Assert.IsType<ExpressionStatement>(statement);
            var identifier = Assert.IsType<Integer>(expression.Expression);
            Assert.Equal(5, identifier.Value);
        }
        
        [Theory]
        [InlineData("true;", true)]
        [InlineData("false;", false)]
        public void GivenAnBooleanExpressionStatement_ThenStatementsAreValid(string script, bool expectedResult)
        {
            var syntaxTree = Parser.ParseScript(script);
            Assert.NotNull(syntaxTree);
            var statement = Assert.Single(syntaxTree.Statements);
            var expression = Assert.IsType<ExpressionStatement>(statement);
            var identifier = Assert.IsType<Boolean>(expression.Expression);
            Assert.Equal(expectedResult, identifier.Value);
        }
        
        [Fact]
        public void GivenAPrefixExpressionStatement_ThenStatementsAreValid()
        {
            const string script = @"!5;";
            var syntaxTree = Parser.ParseScript(script);
            Assert.NotNull(syntaxTree);
            var statement = Assert.Single(syntaxTree.Statements);
            var expression = Assert.IsType<ExpressionStatement>(statement);
            var identifier = Assert.IsType<Prefix>(expression.Expression);
            Assert.Equal("!", identifier.Operator);
            
            var rightExpression  = Assert.IsType<Integer>(identifier.Right);
            Assert.Equal(5, rightExpression.Value);
        }
        
        [Fact]
        public void GivenAInfixExpressionStatement_ThenStatementsAreValid()
        {
            const string script = @"5 + 3;";
            var syntaxTree = Parser.ParseScript(script);
            Assert.NotNull(syntaxTree);
            var statement = Assert.Single(syntaxTree.Statements);
            var expression = Assert.IsType<ExpressionStatement>(statement);
            var identifier = Assert.IsType<Infix>(expression.Expression);
            Assert.Equal("+", identifier.Operator);
            
            var leftExpression  = Assert.IsType<Integer>(identifier.Left);
            Assert.Equal(5, leftExpression.Value);
            
            var rightExpression  = Assert.IsType<Integer>(identifier.Right);
            Assert.Equal(3, rightExpression.Value);
        }
    }
}