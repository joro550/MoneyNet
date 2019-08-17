namespace Monkey.Core.Parsing.Expressions
{
    public enum ExpressionPriority
    {
        NONE,
        LOWEST,
        EQUALS, // ==
        LESS_GREATER, // > or <
        SUM, // +
        PRODUCT, // *
        PREFIX, // -X or !X
        CALL // myFunction(x)
    }
}