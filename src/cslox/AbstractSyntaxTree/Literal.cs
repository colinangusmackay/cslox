// *****************************************
// ** AUTO GENERATED FILE - DO NOT MODIFY **
// *****************************************
//
// Hash: 3af90008b5c68203f672824ff24b2ea0d8742ffa9dac420588d2e2af2d92a5bb
// Date Created: 2025-03-19 18:53:15 UTC
// Last Updated: 2025-03-22 20:06:28 UTC
// --------------------------------------------------------------------------------

namespace cslox.AbstractSyntaxTree;

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
