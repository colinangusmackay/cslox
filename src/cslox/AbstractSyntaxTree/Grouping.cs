// *****************************************
// ** AUTO GENERATED FILE - DO NOT MODIFY **
// *****************************************
//
// Hash: 78a9a05b055ab53f31f863899c6738398f2002dc736126f19e3d8b0af920a9d3
// Date Created: 2025-03-19 18:53:15 UTC
// Last Updated: 2025-03-19 18:53:15 UTC
// --------------------------------------------------------------------------------

namespace cslox.Expressions;

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
