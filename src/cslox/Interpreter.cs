using cslox.Expressions;

namespace cslox;

public class Interpreter : IVisitor<object?>
{
    public void Interpret(Expr expr)
    {
        try
        {
            Object? result = Evaluate(expr);
            Console.WriteLine(Stringify(result));
        }
        catch (RuntimeException e)
        {
            Lox.RuntimeError(e);
        }
    }

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
                CheckNumberOperands(binary.Operator, left, right);
                return (double)left! > (double)right!;
            case TokenType.GreaterEqual:
                CheckNumberOperands(binary.Operator, left, right);
                return (double)left! >= (double)right!;
            case TokenType.Less:
                CheckNumberOperands(binary.Operator, left, right);
                return (double)left! < (double)right!;
            case TokenType.LessEqual:
                CheckNumberOperands(binary.Operator, left, right);
                return (double)left! <= (double)right!;
            case TokenType.Minus:
                CheckNumberOperands(binary.Operator, left, right);
                return (double)left! - (double)right!;
            case TokenType.Plus:
                if (left is double dl && right is double dr)
                    return dl + dr;
                if (left is string sl && right is string sr)
                    return sl + sr;

                throw new RuntimeException(binary.Operator, "Operands must be two numbers or two strings.");
                break;
            case TokenType.Slash:
                CheckNumberOperands(binary.Operator, left, right);
                return (double)left! / (double)right!;
            case TokenType.Star:
                CheckNumberOperands(binary.Operator, left, right);
                return (double)left! * (double)right!;
        }

        return null;
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
                CheckNumberOperand(unary.Operator, right);
                return -(double)right!;
        }

        return null;
    }

    private string Stringify(object? value)
    {
        return (value == null
            ? "nil"
            : value.ToString()) ?? string.Empty;
    }

    private void CheckNumberOperand(Token @operator, object? operand)
    {
        if (operand is double) return;
        throw new RuntimeException(@operator, "Operand must be a number.");
    }

    private void CheckNumberOperands(Token @operator, object? left, object? right)
    {
        if (left is double && right is double) return;
        throw new RuntimeException(@operator, "Operands must be numbers.");
    }

    private bool IsEqual(object? left, object? right)
    {
        if (left == null && right == null) return true;
        if (left == null) return false;

        return left.Equals(right);
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
