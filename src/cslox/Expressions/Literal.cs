// *****************************************
// ** AUTO GENERATED FILE - DO NOT MODIFY **
// *****************************************
//
// Hash: 44720b71046470e14714c6e8d8e778336c1cdb5d93b9326480d554472317fcc3
// Date Created: 2025-03-19 18:53:15 UTC
// Last Updated: 2025-03-19 18:53:15 UTC
// --------------------------------------------------------------------------------

namespace cslox.Expressions;

public class Literal : Expr
{
    public Literal(object? value)
    {
        Value = value;
    }

    public object? Value { get; }

    public override TResult Accept<TResult>(IVisitor<TResult> visitor)
        => visitor.VisitLiteralExpr(this);
}
