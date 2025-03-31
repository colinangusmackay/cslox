using cslox.AbstractSyntaxTree;

namespace cslox.Functions;

public class LoxFunction : ILoxCallable
{
    private readonly Function _declaration;

    public LoxFunction(Function declaration)
    {
        _declaration = declaration;
    }

    public object? Call(Interpreter interpreter, List<object?> arguments)
    {
        InterpreterEnvironment environment = new InterpreterEnvironment(interpreter.Globals);
        for (int i = 0; i < _declaration.Parameters.Count; i++)
        {
            var name = _declaration.Parameters[i].Lexeme;
            var value = arguments[i];
            environment.Define(name, value);
        }

        try
        {
            interpreter.ExecuteBlock(_declaration.Body, environment);
        }
        catch (ReturnControlFlow rcf)
        {
            return rcf.Value;
        }

        return null;
    }

    public int Arity()
        => _declaration.Parameters.Count;

    public override string ToString() => $"<fn:{_declaration.Name.Lexeme}>";
}
