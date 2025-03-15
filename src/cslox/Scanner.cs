namespace cslox;

public class Scanner
{
    private readonly string _source;
    private readonly List<Token> _tokens = [];
    private int _start = 0;
    private int _current = 0;
    private int _line = 1;

    public Scanner(string source)
    {
        _source = source;
    }

    internal List<Token> ScanTokens()
    {
        // while (!IsAtEnd())
        // {
        //     // We are at the beginning of the next lexeme.
        //     _start = _current;
        //     ScanToken();
        // }
        _tokens.Add(new Token(TokenType.Eof, "", null, _line));
        return _tokens;
    }
}
