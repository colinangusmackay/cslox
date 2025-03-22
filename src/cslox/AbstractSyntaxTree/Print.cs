// *****************************************
// ** AUTO GENERATED FILE - DO NOT MODIFY **
// *****************************************
//
// Hash: fcab3b90a5e7765d4cf9d205470609dd45822ffea3bec54c0132c66d9b95be51
// Date Created: 2025-03-22 20:06:28 UTC
// Last Updated: 2025-03-22 20:46:46 UTC
// --------------------------------------------------------------------------------

namespace cslox.AbstractSyntaxTree;

public class Print : Stmt
{
    public Print(Expr expression)
    {
        Expression = expression;
    }

    public Expr Expression { get; }

    public override TResult Accept<TResult>(IStmtVisitor<TResult> visitor)
        => visitor.VisitPrintStmt(this);
}
