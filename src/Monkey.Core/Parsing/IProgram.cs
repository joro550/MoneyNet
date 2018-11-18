using System.Collections.Generic;
using Monkey.Core.Parsing.Statements;

namespace Monkey.Core.Parsing
{
    public interface IProgram
    {
        string TokenLiteral();
        List<IStatement> GetStatements();
        
        IEnumerable<string> Errors { get; }
    }
}