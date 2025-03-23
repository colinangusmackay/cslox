// *****************************************
// ** AUTO GENERATED FILE - DO NOT MODIFY **
// *****************************************
//
// Hash: c8579eaaa1c5f7b59c049808a26853d565e337fd264d89d4336f3c540a5d543a
// Date Created: 2025-03-23 21:32:33 UTC
// Last Updated: 2025-03-23 21:32:33 UTC
// --------------------------------------------------------------------------------

namespace cslox.AbstractSyntaxTree;

public class Variable : Expr
{
    public Variable(Token name)
    {
        Name = name;
    }

    public Token Name { get; }

    public override TResult Accept<TResult>(IExprVisitor<TResult> visitor)
        => visitor.VisitVariableExpr(this);
}
