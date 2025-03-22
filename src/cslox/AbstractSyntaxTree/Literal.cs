// *****************************************
// ** AUTO GENERATED FILE - DO NOT MODIFY **
// *****************************************
//
// Hash: 989aa10c642239fcb70690dbdf4bc4bb3fabbf55399ee101f161fe58efa5b662
// Date Created: 2025-03-19 18:53:15 UTC
// Last Updated: 2025-03-22 20:46:46 UTC
// --------------------------------------------------------------------------------

namespace cslox.AbstractSyntaxTree;

public class Literal : Expr
{
    public Literal(object? value)
    {
        Value = value;
    }

    public object? Value { get; }

    public override TResult Accept<TResult>(IExprVisitor<TResult> visitor)
        => visitor.VisitLiteralExpr(this);
}
