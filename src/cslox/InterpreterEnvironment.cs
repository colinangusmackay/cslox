using System.Diagnostics;

namespace cslox;

public class InterpreterEnvironment
{
    private readonly InterpreterEnvironment? _enclosing;
    private readonly Dictionary<string, object?> _values = new();

    public InterpreterEnvironment()
    {
        _enclosing = null;
    }

    public InterpreterEnvironment(InterpreterEnvironment enclosing)
    {
        _enclosing = enclosing;
    }

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

        if (_enclosing != null)
        {
            return _enclosing.Get(name);
        }

        throw new RuntimeException(name, $"Undefined variable '{name.Lexeme}'.");
    }

    public object? GetAt(int distance, string name)
    {
        return Ancestor(distance)._values[name];
    }

    private InterpreterEnvironment Ancestor(int distance)
    {
        InterpreterEnvironment? environment = this;
        for (int i = 0; i < distance; i++)
        {
            environment = environment._enclosing;
            Debug.Assert(environment != null);
        }

        return environment;
    }

    public void Assign(Token name, object? value)
    {
        if (_values.TryGetValue(name.Lexeme, out var oldValue))
        {
            _values[name.Lexeme] = value;
            return;
        }

        if (_enclosing != null)
        {
            _enclosing.Assign(name, value);
            return;
        }

        throw new RuntimeException(name, $"Undefined variable '{name.Lexeme}'.");
    }
}
