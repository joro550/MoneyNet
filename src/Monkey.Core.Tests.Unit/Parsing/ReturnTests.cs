using Monkey.Core.Parsing;
using Xunit;

namespace Monkey.Core.Tests.Unit.Parsing
{
    public class ReturnTests
    {
        [Fact]
        public void GivenReturnStatements_ThenProgramHasExpectedAmountOfReturnStatements()
        {
            const string script = @"return 5;
return 10;
return 993322;";
            
            var parser = new Parser();
            var program = parser.Parse(script);

            Assert.Equal(3, program.GetStatements().Count);
        }
    }
}