// *****************************************
// ** AUTO GENERATED FILE - DO NOT MODIFY **
// *****************************************
//
// Hash: 23f371564baba60f9c1d213eaae40269919a315ae01e8ab2ba468f1859db6237
// Date Created: 2025-03-19 18:53:15 UTC
// Last Updated: 2025-03-22 20:06:28 UTC
// --------------------------------------------------------------------------------

namespace cslox.AbstractSyntaxTree;

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
