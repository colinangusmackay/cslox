namespace cslox;

public class ReturnControlFlow : ControlFlow
{
    public ReturnControlFlow(object? value)
    {
        Value = value;
    }

    public object? Value { get; }
}