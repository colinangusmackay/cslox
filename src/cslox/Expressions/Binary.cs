// AUTO GENERATED FILE

namespace cslox.Expressions;

public class Binary : Expr
{
    public Binary(Expr left, Token @operator, Expr right)
    {
        Left = left;
        Operator = @operator;
        Right = right;
    }

    public Expr Left { get; }

    public Token Operator { get; }

    public Expr Right { get; }

    public override TResult Accept<TResult>(IVisitor<TResult> visitor)
        => visitor.VisitBinaryExpr(this);
}
