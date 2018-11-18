using Xunit;
using System.Linq;
using Monkey.Core.Parsing;
using Monkey.Core.Parsing.Statements;

namespace Monkey.Core.Tests.Unit.Parsing
{
    public class IdentifierTests
    {
        [Fact]
        public void GivenAnIdentifierWhenParsing_ThenCorrectExpressionIsReturned()
        {
            const string script = @"foobar;";
            
            var parser = new Parser();
            var program = parser.Parse(script);

            var statement = program.GetStatements().First() as ExpressionStatement;
            
            Assert.Single(program.GetStatements());
            Assert.NotNull(statement);
            Assert.Equal("foobar", statement.Expression.TokenLiteral());
        }
    }
}