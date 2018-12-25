using System.Linq;
using Monkey.Core.Parsing;
using Monkey.Core.Parsing.Expressions;
using Monkey.Core.Parsing.Statements;
using Xunit;

namespace Monkey.Core.Tests.Unit.Parsing
{
    public class InfixTests
    {
        [Theory]
        [InlineData("5 = 5;", "=")]
        [InlineData("5 != 5;", "!=")]
        [InlineData("5 < 5;", "<")]
        [InlineData("5 > 5;", ">")]
        [InlineData("5 + 5;", "+")]
        [InlineData("5 - 5;", "-")]
        [InlineData("5 / 5;", "/")]
        [InlineData("5 * 5;", "*")]
        public void GivenAnExpressionWithAnInfix_CorrectExpressionsAreReturned(string script, string expectedOperator)
        {
            var parser = new Parser();
            var program = parser.Parse(script);

            var statement = program.GetStatements().First() as ExpressionStatement;
            
            Assert.NotNull(statement);
            var statementExpression = statement.Expression as InfixExpression;
            Assert.NotNull(statementExpression);
            TestIsIntegerLiteral(statementExpression.Left, 5);
            TestIsIntegerLiteral(statementExpression.Right, 5);
            Assert.Equal(expectedOperator, statementExpression.Operator);
        }

        private static void TestIsIntegerLiteral(IExpression expression, int value)
        {
            var integerExpression = expression as IntegerExpression;
            Assert.NotNull(integerExpression);
            Assert.Equal(value, integerExpression.Value);
        }
    }
}