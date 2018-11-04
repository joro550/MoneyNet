using System.Collections.Generic;
using System.Linq;
using Monkey.Core.Lexing;

namespace Monkey.Core.Parsing
{
    public interface INode
    {
        string TokenLiteral();
    }

    public interface IStatement : INode
    {
    }

    public interface IExpression : INode
    {
    }
    
    public class NullStatement : IStatement
    {
        public string TokenLiteral() => string.Empty;
    }

    public class LetStatement : IStatement
    {
        public LetStatement(Token token)
        {
            Token = token;
        }

        public Token Token { get; }
        public string Name { get; set; }
        public IExpression Value { get; set; }
        
        public string TokenLiteral() 
            => Token.Value;
    }

    public interface IProgram
    {
        string TokenLiteral();
        List<IStatement> GetStatements();
    }

    public class Program : INode, IProgram
    {
        private readonly List<IStatement> _statements;

        public Program(List<IStatement> statements)
        {
            _statements = statements;
        }

        public string TokenLiteral() 
            => _statements.Count > 1 ? _statements.First().TokenLiteral() : string.Empty;

        public List<IStatement> GetStatements() 
            => _statements;
    }
}