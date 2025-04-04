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
        throw new NotImplementedException();
    }

    public Unit VisitBinaryExpr(Binary binary)
    {
        throw new NotImplementedException();
    }

    public Unit VisitCallExpr(Call call)
    {
        throw new NotImplementedException();
    }

    public Unit VisitGroupingExpr(Grouping grouping)
    {
        throw new NotImplementedException();
    }

    public Unit VisitLiteralExpr(Literal literal)
    {
        throw new NotImplementedException();
    }

    public Unit VisitLogicalExpr(Logical logical)
    {
        throw new NotImplementedException();
    }

    public Unit VisitUnaryExpr(Unary unary)
    {
        throw new NotImplementedException();
    }

    public Unit VisitVariableExpr(Variable variable)
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }

    public Unit VisitFunctionStmt(Function function)
    {
        throw new NotImplementedException();
    }

    public Unit VisitIfStmt(If @if)
    {
        throw new NotImplementedException();
    }

    public Unit VisitPrintStmt(Print print)
    {
        throw new NotImplementedException();
    }

    public Unit VisitReturnStmt(Return @return)
    {
        throw new NotImplementedException();
    }

    public Unit VisitVarStmt(Var var)
    {
        throw new NotImplementedException();
    }

    public Unit VisitWhileStmt(While @while)
    {
        throw new NotImplementedException();
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
}
