namespace cslox;

public class ParserException : Exception
{
    public ParserException(Token token, string message)
        : base($"Parser error on line {token.Line} at {(token.Type == TokenType.Eof ? "end" : $"'{token.Lexeme}'")}.  {message}")
    {
    }
}