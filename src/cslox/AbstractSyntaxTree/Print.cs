// *****************************************
// ** AUTO GENERATED FILE - DO NOT MODIFY **
// *****************************************
//
// Hash: e2e38af2a6c6aeb52bc7fd2b015570032c2080e2cc1a9e353b83e170ca0ae653
// Date Created: 2025-03-22 20:06:28 UTC
// Last Updated: 2025-03-22 20:06:28 UTC
// --------------------------------------------------------------------------------

namespace cslox.AbstractSyntaxTree;

public class Print : Stmt
{
    public Print(Expr expression)
    {
        Expression = expression;
    }

    public Expr Expression { get; }

    public override TResult Accept<TResult>(IVisitor<TResult> visitor)
        => visitor.VisitPrintStmt(this);
}
