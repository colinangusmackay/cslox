// *****************************************
// ** AUTO GENERATED FILE - DO NOT MODIFY **
// *****************************************
//
// Hash: e31e93f8d435767d318056dff4085fdbc5fd810e64f619fbde6335a608d74c7c
// Date Created: 2025-03-19 17:27:18 UTC
// Last Updated: 2025-03-22 20:45:57 UTC
// --------------------------------------------------------------------------------

namespace cslox.AbstractSyntaxTree;

public abstract class Expr
{
    public abstract TResult Accept<TResult>(IExprVisitor<TResult> visitor);
}
