namespace cslox.Functions.NativeFunctions;

public class ToStringFunction : ILoxCallable
{
    public object? Call(Interpreter interpreter, List<object?> arguments)
        => arguments[0]?.ToString();

    public int Arity() => 1;

    public override string ToString() => "<native fn:toString>";
}