using System.Collections.Generic;
using Monkey.Core.Parsing.Statements;

namespace Monkey.Core.Parsing
{
    public class BlockStatement 
    {
        public List<Statement> Statements { get; } = new List<Statement>();
    }
}