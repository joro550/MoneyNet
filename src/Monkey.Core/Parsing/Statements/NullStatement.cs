namespace Monkey.Core.Parsing.Statements
{
    public class NullStatement : IStatement
    {
        public NullStatement(string error)
        {
            Error = error;
        }

        public string Error { get; }

        public string TokenLiteral()
        {
            return string.Empty;
        }
    }
}