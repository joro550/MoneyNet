using System.Linq;
using Monkey.Core.Parsing;
using Monkey.Core.Parsing.Expressions;
using Monkey.Core.Parsing.Statements;
using Xunit;

namespace Monkey.Core.Tests.Unit.Parsing
{
    public class IntegerTests
    {
        [Fact]
        public void GivenAnIdentifierWhenParsing_ThenCorrectExpressionIsReturned()
        {
            const string script = @"5;";
            
            var parser = new Parser();
            var program = parser.Parse(script);

            var statement = program.GetStatements().First() as ExpressionStatement;
            var expression = statement?.Expression as IntegerStatement;

            Assert.NotNull(statement);
            Assert.NotNull(expression);
            Assert.Single(program.GetStatements());
            Assert.Equal(5, expression.Value);
        }
    }
}