using System.Collections.Generic;
using System.Linq;

namespace Monkey.Core.Parsing
{
    public class Program
    {
        private readonly List<IStatement> _statements;

        public Program() 
            => _statements = new List<IStatement>();

        public void AddStatement(IStatement statement) 
            => _statements.Add(statement);

        public IEnumerable<IStatement> GetStatements() 
            => _statements;

        public List<string> Errors() 
            => _statements.SelectMany(statement => statement.Errors).ToList();
    }
}