namespace cslox;

public class InterpreterEnvironment
{
    private readonly Dictionary<string, object?> _values = new();
    
    public void Define(string name, object? value)
    {
        _values[name] = value;
    }

    public object? Get(Token name)
    {
        if (_values.TryGetValue(name.Lexeme, out var value))
        {
            return value;
        }
        throw new RuntimeException(name, $"Undefined variable '{name.Lexeme}'.");
    }

    public void Assign(Token name, object? value)
    {
        if (_values.TryGetValue(name.Lexeme, out var oldValue))
        {
            _values[name.Lexeme] = value;
            return;
        }

        throw new RuntimeException(name, $"Undefined variable '{name.Lexeme}'.");
    }
}
