using cslox.Expressions;

namespace cslox;

public class Interpreter : IVisitor<object?>
{
    public object? VisitBinaryExpr(Binary binary)
    {
        throw new NotImplementedException();
    }

    public object? VisitGroupingExpr(Grouping grouping)
    {
        throw new NotImplementedException();
    }

    public object? VisitLiteralExpr(Literal literal)
    {
        throw new NotImplementedException();
    }

    public object? VisitUnaryExpr(Unary unary)
    {
        throw new NotImplementedException();
    }
}
