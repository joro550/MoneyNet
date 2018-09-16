using System.Collections.Generic;
using Monkey.Core.Lexing;

namespace Monkey.Core.Parsing
{
    public interface IStatement : INode
    {
        List<string> Errors { get; }
        Token Token { get; }
        Identifier Name { get; }
        IExpressioin Value { get; }
        
        void StatementNode();
    }
}