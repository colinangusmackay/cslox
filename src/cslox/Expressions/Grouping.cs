// AUTO GENERATED FILE

namespace cslox.Expressions;

public class Grouping : Expr
{
    public Grouping(Expr expression)
    {
        Expression = expression;
    }

    public Expr Expression { get; }

    public override TResult Accept<TResult>(IVisitor<TResult> visitor)
        => visitor.VisitGroupingExpr(this);
}
