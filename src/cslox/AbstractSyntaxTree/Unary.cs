// *****************************************
// ** AUTO GENERATED FILE - DO NOT MODIFY **
// *****************************************
//
// Hash: fa206a8d4e87002b5940e122af929d2f6d884be0df622f9e8c7202dc9138928b
// Date Created: 2025-03-19 18:53:15 UTC
// Last Updated: 2025-03-22 20:46:46 UTC
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

    public override TResult Accept<TResult>(IExprVisitor<TResult> visitor)
        => visitor.VisitUnaryExpr(this);
}
