// *****************************************
// ** AUTO GENERATED FILE - DO NOT MODIFY **
// *****************************************
//
// Hash: 6bccce667b11d58487e83974411a3725c50378be0c7443c2b709ef872b010eab
// Date Created: 2025-03-19 18:53:15 UTC
// Last Updated: 2025-03-22 20:46:46 UTC
// --------------------------------------------------------------------------------

namespace cslox.AbstractSyntaxTree;

public class Grouping : Expr
{
    public Grouping(Expr expression)
    {
        Expression = expression;
    }

    public Expr Expression { get; }

    public override TResult Accept<TResult>(IExprVisitor<TResult> visitor)
        => visitor.VisitGroupingExpr(this);
}
