using System.Collections.Generic;
using System.Linq;
using Monkey.Core.Parsing;
using Xunit;

namespace Monkey.Core.Tests.Unit.Parsing
{
    public class ParseTests
    {
        [Fact]
        public void GivenALetStatements_ThenProgramHasOnlyLetStatements()
        {
            const string script = @"let x = 5;
let y = 10;
let foobar = 1000;";
            var parser = new Parser();
            var program = parser.Parse(script);

            var expectedIdentifiers = new List<string> {"x", "y", "foobar" };
            Assert.Equal(3, program.GetStatements().Count);
            foreach (var identifier in expectedIdentifiers)
                Assert.Contains(identifier,
                    program.GetStatements().OfType<LetStatement>().Select(statement => statement.Name));
        }

        [Fact]
        public void GivenInvalidLetStatement_ThenCorrectErrorsAreReturned()
        {
            const string script = "let x 5;";
            
            var parser = new Parser();
            var program = parser.Parse(script);
            Assert.Single(program.Errors);
            Assert.Equal("Expected '=' but have INT", program.Errors.Single());
        }
    }
}