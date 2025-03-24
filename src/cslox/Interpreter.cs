using cslox.AbstractSyntaxTree;

namespace cslox;

public class Interpreter : IExprVisitor<object?>, IStmtVisitor<Unit>
{
    private readonly InterpreterEnvironment _interpreterEnvironment = new();

    public void Interpret(List<Stmt> statements)
    {
        try
        {
            foreach (Stmt statement in statements)
                Execute(statement);
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

    public object? VisitVariableExpr(Variable variable)
    {
        return _interpreterEnvironment.Get(variable.Name);
    }

    public Unit VisitExpressionStmt(Expression expression)
    {
        Evaluate(expression.InnerExpression);
        return Unit.Value;
    }

    public Unit VisitPrintStmt(Print print)
    {
        var value = Evaluate(print.Expression);
        Console.WriteLine(Stringify(value));
        return Unit.Value;
    }

    public Unit VisitVarStmt(Var var)
    {
        Object? value = null;
        if (var.Initializer != null)
            value = Evaluate(var.Initializer);

        _interpreterEnvironment.Define(var.Name.Lexeme, value);
        return Unit.Value;
    }

    public object? VisitAssignExpr(Assign assign)
    {
        object? value = Evaluate(assign.Value);
        _interpreterEnvironment.Assign(assign.Name, value);
        return value;
    }

    private void Execute(Stmt stmt)
    {
        stmt.Accept(this);
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
