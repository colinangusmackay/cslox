namespace cslox.Functions.NativeFunctions;

public class Clock : ILoxCallable
{
    public object? Call(Interpreter interpreter, List<object?> arguments)
        => (DateTime.UtcNow.Ticks - DateTime.UnixEpoch.Ticks) / (double)TimeSpan.TicksPerSecond;

    public int Arity() => 0;

    public override string ToString() => "<native fn:clock>";
}
