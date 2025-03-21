namespace cslox;

public class RuntimeException : Exception
{
    public RuntimeException(Token token, string message)
        : base($"Runtime error on line {token.Line} at {(token.Type == TokenType.Eof ? "end" : $"'{token.Lexeme}'")}.  {message}")
    {
        Token = token;
    }

    public Token Token { get; }
}