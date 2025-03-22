// *****************************************
// ** AUTO GENERATED FILE - DO NOT MODIFY **
// *****************************************
//
// Hash: aff313b3739df6da581a3b1bdc780049c2c686010470a25c49540daba1e179a2
// Date Created: 2025-03-19 18:53:15 UTC
// Last Updated: 2025-03-22 20:06:28 UTC
// --------------------------------------------------------------------------------

namespace cslox.AbstractSyntaxTree;

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
