// *****************************************
// ** AUTO GENERATED FILE - DO NOT MODIFY **
// *****************************************
//
// Hash: 8bec3a3fca53979ccb99ff98c98160cf51d52b529ffc026a91d56f0c79e911ea
// Date Created: 2025-03-19 18:53:15 UTC
// Last Updated: 2025-03-22 20:46:46 UTC
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

    public override TResult Accept<TResult>(IExprVisitor<TResult> visitor)
        => visitor.VisitBinaryExpr(this);
}
