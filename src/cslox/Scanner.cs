using System.Collections.Frozen;
using System.Runtime.InteropServices.JavaScript;

namespace cslox;

public class Scanner
{
    private static readonly FrozenDictionary<string, TokenType> Keywords = new Dictionary<string, TokenType>
    {
        { "and", TokenType.And },
        { "class", TokenType.Class },
        { "else", TokenType.Else },
        { "false", TokenType.False },
        { "for", TokenType.For },
        { "fun", TokenType.Fun },
        { "if", TokenType.If },
        { "nil", TokenType.Nil },
        { "or", TokenType.Or },
        { "print", TokenType.Print },
        { "return", TokenType.Return },
        { "super", TokenType.Super },
        { "this", TokenType.This },
        { "true", TokenType.True },
        { "var", TokenType.Var },
        { "while", TokenType.While },
    }.ToFrozenDictionary();

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
        while (!IsAtEnd())
        {
            // We are at the beginning of the next lexeme.
            _start = _current;
            ScanToken();
        }
        _tokens.Add(new Token(TokenType.Eof, "", null, _line));
        return _tokens;
    }

    private bool IsAtEnd()
    {
        return _current >= _source.Length;
    }

    private void ScanToken()
    {
        char c = Advance();
        switch (c)
        {
            case '(': AddToken(TokenType.LeftParen); break;
            case ')': AddToken(TokenType.RightParen); break;
            case '{': AddToken(TokenType.LeftBrace); break;
            case '}': AddToken(TokenType.RightBrace); break;
            case ',': AddToken(TokenType.Comma); break;
            case '.': AddToken(TokenType.Dot); break;
            case '-': AddToken(TokenType.Minus); break;
            case '+': AddToken(TokenType.Plus); break;
            case ';': AddToken(TokenType.Semicolon); break;
            case '*': AddToken(TokenType.Star); break;
            case '!': AddToken(Match('=') ? TokenType.BangEqual : TokenType.Bang); break;
            case '=': AddToken(Match('=') ? TokenType.EqualEqual : TokenType.Equal); break;
            case '<': AddToken(Match('=') ? TokenType.LessEqual : TokenType.Less); break;
            case '>': AddToken(Match('=') ? TokenType.GreaterEqual : TokenType.Greater); break;
            case '/':
                if (Match('/'))
                {
                    while(Peek() != '\n' && !IsAtEnd()) Advance();
                }
                else
                {
                    AddToken(TokenType.Slash);
                }

                break;
            case ' ':
            case '\r':
            case '\t':
                break;
            case '\n':
                _line++;
                break;
            case '"':
                String();
                break;
            case >= '0' and <= '9':
                Number();
                break;
            case >= 'a' and <= 'z':
            case >= 'A' and <= 'Z':
            case '_':
                Identifier();
                break;
            default:
                Lox.Error(_line, "Unexpected character.");
                break;
        }
    }

    private void Identifier()
    {
        while(IsAlphaNumeric(Peek())) Advance();
        var text = _source[_start.._current];
        AddToken(Keywords.GetValueOrDefault(text, TokenType.Identifier));
    }

    private void Number()
    {
        while(IsDigit(Peek())) Advance();
        if (Peek() == '.' && IsDigit(PeekNext()))
            Advance();
        while (IsDigit(Peek())) Advance();

        AddToken(TokenType.Number, double.Parse(_source[_start.._current]));
    }

    private char PeekNext()
        => _current + 1 >= _source.Length
            ? '\0'
            : _source[_current + 1];

    private static bool IsDigit(char c)
        => char.IsBetween(c, '0', '9');

    private static bool IsAlpha(char c)
        => char.IsBetween(c, 'a', 'z') || char.IsBetween(c, 'A', 'Z') || c == '_';

    private static bool IsAlphaNumeric(char c)
        => IsAlpha(c) || IsDigit(c);

    private char Advance()
        => _source[_current++];

    private void AddToken(TokenType type)
        => AddToken(type, null);

    private void AddToken(TokenType type, object? literal)
    {
        string text = _source[_start.._current];
        _tokens.Add(new Token(type, text, literal, _line));
    }

    private bool Match(char expected)
    {
        if (IsAtEnd()) return false;
        if (_source[_current] != expected) return false;
        _current++;
        return true;
    }

    private char Peek()
        => IsAtEnd()
            ? '\0'
            : _source[_current];

    private void String()
    {
        while (Peek() != '"' && !IsAtEnd())
        {
            if (Peek() == '\n') _line++;
            Advance();
        }

        if (IsAtEnd())
        {
            Lox.Error(_line, "Unterminated string.");
            return;
        }

        // Advance past the closing quote character.
        Advance();
        string value = _source[(_start + 1)..(_current - 1)];
        AddToken(TokenType.String, value);
    }
}
