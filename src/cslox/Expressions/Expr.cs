// AUTO GENERATED FILE

namespace cslox.Expressions;

public abstract class Expr
{
    public abstract TResult Accept<TResult>(IVisitor<TResult> visitor);
}
