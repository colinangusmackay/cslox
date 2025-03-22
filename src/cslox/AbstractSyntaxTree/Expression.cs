// *****************************************
// ** AUTO GENERATED FILE - DO NOT MODIFY **
// *****************************************
//
// Hash: 2698d621f7f120659788e225a4cf4c2ba8d46fa0793743f679e5a8342d05129c
// Date Created: 2025-03-22 20:06:28 UTC
// Last Updated: 2025-03-22 20:34:19 UTC
// --------------------------------------------------------------------------------

namespace cslox.AbstractSyntaxTree;

public class Expression : Stmt
{
    public Expression(Expr innerexpression)
    {
        InnerExpression = innerexpression;
    }

    public Expr InnerExpression { get; }

    public override TResult Accept<TResult>(IVisitor<TResult> visitor)
        => visitor.VisitExpressionStmt(this);
}
