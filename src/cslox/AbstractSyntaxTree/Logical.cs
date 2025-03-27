// *****************************************
// ** AUTO GENERATED FILE - DO NOT MODIFY **
// *****************************************
//
// Hash: bdf1ff29447d7b47c875b2c6e49c3e14ac07faab720aea2a1d57195e11935638
// Date Created: 2025-03-27 20:44:42 UTC
// Last Updated: 2025-03-27 20:44:42 UTC
// --------------------------------------------------------------------------------

namespace cslox.AbstractSyntaxTree;

public class Logical : Expr
{
    public Logical(Expr left, Token @operator, Expr right)
    {
        Left = left;
        Operator = @operator;
        Right = right;
    }

    public Expr Left { get; }

    public Token Operator { get; }

    public Expr Right { get; }

    public override TResult Accept<TResult>(IExprVisitor<TResult> visitor)
        => visitor.VisitLogicalExpr(this);
}
