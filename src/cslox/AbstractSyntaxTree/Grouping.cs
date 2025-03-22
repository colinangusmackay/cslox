// *****************************************
// ** AUTO GENERATED FILE - DO NOT MODIFY **
// *****************************************
//
// Hash: 4dd8b12836a5ace1dfee6640f0df63dc71845f333957b2dcd4c1cc0987356cc4
// Date Created: 2025-03-19 18:53:15 UTC
// Last Updated: 2025-03-22 20:06:28 UTC
// --------------------------------------------------------------------------------

namespace cslox.AbstractSyntaxTree;

public class Grouping : Expr
{
    public Grouping(Expr expression)
    {
        Expression = expression;
    }

    public Expr Expression { get; }

    public override TResult Accept<TResult>(IVisitor<TResult> visitor)
        => visitor.VisitGroupingExpr(this);
}
