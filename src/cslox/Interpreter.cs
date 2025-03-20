using cslox.Expressions;

namespace cslox;

public class Interpreter : IVisitor<object?>
{
    public object? VisitBinaryExpr(Binary binary)
    {
        throw new NotImplementedException();
    }

    public object? VisitGroupingExpr(Grouping grouping)
        => Evaluate(grouping.Expression);

    public object? VisitLiteralExpr(Literal literal)
        => literal.Value;

    public object? VisitUnaryExpr(Unary unary)
    {
        object? right = Evaluate(unary.Right);
        switch (unary.Operator.Type)
        {
            case TokenType.Bang:
                return !IsTruthy(right);
            case TokenType.Minus:
                return -(double)right!;
        }

        return null;
    }

    private bool IsTruthy(object? right)
    {
        throw new NotImplementedException();
    }

    private object? Evaluate(Expr expr)
        => expr.Accept(this);
}
