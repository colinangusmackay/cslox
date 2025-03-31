using cslox.AbstractSyntaxTree;
using cslox.Functions.NativeFunctions;

namespace cslox;

public class Interpreter : IExprVisitor<object?>, IStmtVisitor<Unit>
{
    private readonly InterpreterEnvironment _globalEnvironment = new();
    private InterpreterEnvironment _interpreterEnvironment;

    public Interpreter()
    {
        _interpreterEnvironment = _globalEnvironment;

        _globalEnvironment.Define("clock", new Clock());
    }

    public InterpreterEnvironment Globals => _globalEnvironment;

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

    public object? VisitCallExpr(Call call)
    {
        Object? callee = Evaluate(call.Callee);
        List<object?> arguments = new();
        foreach (Expr argument in call.Arguments)
        {
            arguments.Add(Evaluate(argument));
        }

        if (callee is not ILoxCallable function)
        {
            throw new RuntimeException(call.Paren, "Can only call functions and classes.");
        }

        if (arguments.Count != function.Arity())
        {
            throw new RuntimeException(call.Paren, $"Expected {function.Arity()} arguments but got {arguments.Count}.");
        }

        return function.Call(this, arguments);
    }

    public object? VisitGroupingExpr(Grouping grouping)
        => Evaluate(grouping.Expression);

    public object? VisitLiteralExpr(Literal literal)
        => literal.Value;

    public object? VisitLogicalExpr(Logical logical)
    {
        var left = Evaluate(logical.Left);
        if (logical.Operator.Type == TokenType.Or)
        {
            // left || right
            if (IsTruthy(left)) return left;
        }
        else
        {
            // left && right
            if (!IsTruthy(left)) return left;
        }

        return Evaluate(logical.Right);
    }

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

    public Unit VisitBlockStmt(Block block)
    {
        ExecuteBlock(block.Statements, new InterpreterEnvironment(_interpreterEnvironment));
        return Unit.Value;
    }

    public Unit VisitExpressionStmt(Expression expression)
    {
        Evaluate(expression.InnerExpression);
        return Unit.Value;
    }

    public Unit VisitIfStmt(If @if)
    {
        if (IsTruthy(Evaluate(@if.Condition)))
            Execute(@if.ThenBranch);
        else if (@if.ElseBranch != null)
            Execute(@if.ElseBranch);

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

    public Unit VisitWhileStmt(While @while)
    {
        while (IsTruthy(Evaluate(@while.Condition)))
        {
            Execute(@while.Body);
        }

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

    public void ExecuteBlock(List<Stmt> statements, InterpreterEnvironment environment)
    {
        InterpreterEnvironment previous = _interpreterEnvironment;
        try
        {
            _interpreterEnvironment = environment;
            foreach (Stmt statement in statements)
                Execute(statement);
        }
        finally
        {
            _interpreterEnvironment = previous;
        }
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
