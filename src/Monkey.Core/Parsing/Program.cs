using System.Collections.Generic;
using System.Linq;
using Monkey.Core.Parsing.Statements;

namespace Monkey.Core.Parsing
{
    public class Program : INode, IProgram
    {
        private readonly List<IStatement> _statements;

        public Program(List<IStatement> statements) 
            => _statements = statements;

        public string TokenLiteral() 
            => _statements.Count > 1 ? _statements.First().TokenLiteral() : string.Empty;

        public List<IStatement> GetStatements() 
            => _statements;

        public IEnumerable<string> Errors 
            => _statements.OfType<NullStatement>().Select(statement => statement.Error);
    }
}