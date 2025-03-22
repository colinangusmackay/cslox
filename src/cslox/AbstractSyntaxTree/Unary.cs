// *****************************************
// ** AUTO GENERATED FILE - DO NOT MODIFY **
// *****************************************
//
// Hash: f39b81ac921a46c948ff237679cb81d5ff2bf1a495fdc613602d1c31b1047ed3
// Date Created: 2025-03-19 18:53:15 UTC
// Last Updated: 2025-03-19 18:53:15 UTC
// --------------------------------------------------------------------------------

namespace cslox.Expressions;

public class Unary : Expr
{
    public Unary(Token @operator, Expr right)
    {
        Operator = @operator;
        Right = right;
    }

    public Token Operator { get; }

    public Expr Right { get; }

    public override TResult Accept<TResult>(IVisitor<TResult> visitor)
        => visitor.VisitUnaryExpr(this);
}
