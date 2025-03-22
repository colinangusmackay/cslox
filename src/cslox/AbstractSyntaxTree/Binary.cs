// *****************************************
// ** AUTO GENERATED FILE - DO NOT MODIFY **
// *****************************************
//
// Hash: 210c9723cee10475d4d6fe2ee1027f42f5f342affa4025f5c6d588cf7adb2d1d
// Date Created: 2025-03-19 18:53:15 UTC
// Last Updated: 2025-03-19 18:53:15 UTC
// --------------------------------------------------------------------------------

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
