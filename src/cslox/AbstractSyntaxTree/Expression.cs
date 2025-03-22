// *****************************************
// ** AUTO GENERATED FILE - DO NOT MODIFY **
// *****************************************
//
// Hash: 01c46b508c5594de920feb78f23d1491da363e68b57136e24d49cb831a4704cb
// Date Created: 2025-03-22 20:06:28 UTC
// Last Updated: 2025-03-22 20:46:46 UTC
// --------------------------------------------------------------------------------

namespace cslox.AbstractSyntaxTree;

public class Expression : Stmt
{
    public Expression(Expr innerexpression)
    {
        InnerExpression = innerexpression;
    }

    public Expr InnerExpression { get; }

    public override TResult Accept<TResult>(IStmtVisitor<TResult> visitor)
        => visitor.VisitExpressionStmt(this);
}
