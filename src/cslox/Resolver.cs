using cslox.AbstractSyntaxTree;

namespace cslox;

public class Resolver : IExprVisitor<Unit>, IStmtVisitor<Unit>
{
    private readonly Interpreter _interpreter;
    private readonly Stack<Dictionary<string, bool>> _scopes = new();

    public Resolver(Interpreter interpreter)
    {
        _interpreter = interpreter;
    }

    public Unit VisitAssignExpr(Assign assign)
    {
        Resolve(assign.Value);
        ResolveLocal(assign, assign.Name);
        return Unit.Value;
    }

    public Unit VisitBinaryExpr(Binary binary)
    {
        Resolve(binary.Left);
        Resolve(binary.Right);
        return Unit.Value;
    }

    public Unit VisitCallExpr(Call call)
    {
        Resolve(call.Callee);
        foreach (var argument in call.Arguments)
        {
            Resolve(argument);
        }

        return Unit.Value;
    }

    public Unit VisitGroupingExpr(Grouping grouping)
    {
        Resolve(grouping.Expression);
        return Unit.Value;
    }

    public Unit VisitLiteralExpr(Literal literal)
    {
        return Unit.Value;
    }

    public Unit VisitLogicalExpr(Logical logical)
    {
        Resolve(logical.Left);
        Resolve(logical.Right);
        return Unit.Value;
    }

    public Unit VisitUnaryExpr(Unary unary)
    {
        Resolve(unary.Right);
        return Unit.Value;
    }

    public Unit VisitVariableExpr(Variable variable)
    {
        if (_scopes.Count > 0)
        {
            var scope = _scopes.Peek();
            var isDefined = scope.TryGetValue(variable.Name.Lexeme, out var isDefinedInScope);
            if (isDefined && !isDefinedInScope)
            {
                Lox.Error(variable.Name, $"Cannot read local variable '{variable.Name.Lexeme}' in its own initializer.");
            }
        }

        ResolveLocal(variable, variable.Name);
        return Unit.Value;
    }

    public Unit VisitBlockStmt(Block block)
    {
        BeginScope();
        Resolve(block.Statements);
        EndScope();
        return Unit.Value;
    }

    public Unit VisitExpressionStmt(Expression expression)
    {
        Resolve(expression.InnerExpression);
        return Unit.Value;
    }

    public Unit VisitFunctionStmt(Function function)
    {
        Declare(function.Name);
        Define(function.Name);

        ResolveFunction(function);
        return Unit.Value;
    }

    public Unit VisitIfStmt(If @if)
    {
        Resolve(@if.Condition);
        Resolve(@if.ThenBranch);
        if (@if.ElseBranch != null)
            Resolve(@if.ElseBranch);
        return Unit.Value;
    }

    public Unit VisitPrintStmt(Print print)
    {
        Resolve(print.Expression);
        return Unit.Value;
    }

    public Unit VisitReturnStmt(Return @return)
    {
        if (@return.Value != null)
            Resolve(@return.Value);
        return Unit.Value;
    }

    public Unit VisitVarStmt(Var var)
    {
        Declare(var.Name);
        if (var.Initializer != null)
            Resolve(var.Initializer);
        Define(var.Name);
        return Unit.Value;
    }

    public Unit VisitWhileStmt(While @while)
    {
        Resolve(@while.Condition);
        Resolve(@while.Body);
        return Unit.Value;
    }

    private void Resolve(List<Stmt> statements)
    {
        foreach (var stmt in statements)
        {
            Resolve(stmt);
        }
    }

    private void Resolve(Stmt statement)
    {
        statement.Accept(this);
    }

    private void Resolve(Expr expression)
    {
        expression.Accept(this);
    }

    private void BeginScope()
    {
        _scopes.Push(new Dictionary<string, bool>());
    }

    private void EndScope()
    {
        _scopes.Pop();
    }

    private void Declare(Token name)
    {
        if (_scopes.Count == 0) return;

        var scope = _scopes.Peek();
        scope.Add(name.Lexeme, false);
    }

    private void Define(Token name)
    {
        if (_scopes.Count == 0) return;
        _scopes.Peek()[name.Lexeme] = true;
    }

    private void ResolveLocal(Expr expr, Token name)
    {
        for (int i = _scopes.Count - 1; i >= 0; i--)
        {
            if (_scopes.ElementAt(i).ContainsKey(name.Lexeme))
            {
                _interpreter.Resolve(expr, _scopes.Count - 1 - i);
                return;
            }
        }
    }

    private void ResolveFunction(Function function)
    {
        BeginScope();
        foreach (var param in function.Parameters)
        {
            Declare(param);
            Define(param);
        }
        Resolve(function.Body);
        EndScope();
    }
}
