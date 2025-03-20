using cslox.Expressions;

namespace cslox;

public class Interpreter : IVisitor<object?>
{
    public object? VisitBinaryExpr(Binary binary)
    {
        object? left = Evaluate(binary.Left);
        object? right = Evaluate(binary.Right);

        switch (binary.Operator.Type)
        {
            case TokenType.BangEqual:
                return !IsEqual(left, right);
            case TokenType.EqualEqual:
                return IsEqual(left, right);
            case TokenType.Greater:
                return (double)left! > (double)right!;
            case TokenType.GreaterEqual:
                return (double)left! >= (double)right!;
            case TokenType.Less:
                return (double)left! < (double)right!;
            case TokenType.LessEqual:
                return (double)left! <= (double)right!;
            case TokenType.Minus:
                return (double)left! - (double)right!;
            case TokenType.Plus:
                if (left is double dl && right is double dr)
                    return dl + dr;
                if (left is string sl && right is string sr)
                    return sl + sr;
                break;
            case TokenType.Slash:
                return (double)left! / (double)right!;
            case TokenType.Star:
                return (double)left! * (double)right!;
        }

        return null;
    }

    private bool IsEqual(object? left, object? right)
    {
        if (left == null && right == null) return true;
        if (left == null) return false;

        return left.Equals(right);
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

    private bool IsTruthy(object? value)
        => value switch
        {
            // Following the Ruby rules of truthiness:
            // Boolean false and nil are falsey.
            // Everything else is truthy.
            null => false,
            bool b => b,
            _ => true
        };

    private object? Evaluate(Expr expr)
        => expr.Accept(this);
}
