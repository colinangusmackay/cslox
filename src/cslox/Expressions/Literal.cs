// AUTO GENERATED FILE

namespace cslox.Expressions;

public class Literal : Expr
{
    public Literal(object value)
    {
        Value = value;
    }

    public object Value { get; }

    public override TResult Accept<TResult>(IVisitor<TResult> visitor)
        => visitor.VisitLiteralExpr(this);
}
