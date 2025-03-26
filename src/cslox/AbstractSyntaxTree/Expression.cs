// *****************************************
// ** AUTO GENERATED FILE - DO NOT MODIFY **
// *****************************************
//
// Hash: 5e1bf29c7bcfac2a264ddba220e044cb89217c17fdc7a380152a8b6cbd13974d
// Date Created: 2025-03-22 20:06:28 UTC
// Last Updated: 2025-03-26 21:31:09 UTC
// --------------------------------------------------------------------------------

namespace cslox.AbstractSyntaxTree;

public class Expression : Stmt
{
    public Expression(Expr innerExpression)
    {
        InnerExpression = innerExpression;
    }

    public Expr InnerExpression { get; }

    public override TResult Accept<TResult>(IStmtVisitor<TResult> visitor)
        => visitor.VisitExpressionStmt(this);
}
