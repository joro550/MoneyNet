using System.Linq;
using Monkey.Core.Parsing;
using Monkey.Core.Parsing.Expressions;
using Monkey.Core.Parsing.Statements;
using Xunit;

namespace Monkey.Core.Tests.Unit.Parsing
{
    public class PrefixTests
    {
        [Theory]
        [InlineData("-5;", "-")]
        [InlineData("!5;", "!")]
        public void GivenAnIdentifierWhenParsing_ThenCorrectExpressionIsReturned(string script, string expectedOperator)
        {
            var parser = new Parser();
            var program = parser.Parse(script);
            var statement = program.GetStatements().First() as ExpressionStatement;
            var expression = statement?.Expression as PrefixExpression;

            Assert.NotNull(statement);
            Assert.NotNull(expression);
            Assert.Equal(expectedOperator, expression.Operator);
            Assert.IsType<IntegerExpression>(expression.Right);
        }
    }
}