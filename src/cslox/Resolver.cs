using cslox.AbstractSyntaxTree;

namespace cslox;

public class Resolver : IExprVisitor<Unit>, IStmtVisitor<Unit>
{
    private readonly Interpreter _interpreter;

    public Resolver(Interpreter interpreter)
    {
        _interpreter = interpreter;
    }
}
