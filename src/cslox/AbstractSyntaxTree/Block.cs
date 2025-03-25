// *****************************************
// ** AUTO GENERATED FILE - DO NOT MODIFY **
// *****************************************
//
// Hash: 3412f2a714e68d931a184fc01f1f257058a7887eaf7f4d515bd3825555f75bb6
// Date Created: 2025-03-25 22:50:37 UTC
// Last Updated: 2025-03-25 22:50:37 UTC
// --------------------------------------------------------------------------------

namespace cslox.AbstractSyntaxTree;

public class Block : Stmt
{
    public Block(List<Stmt> statements)
    {
        Statements = statements;
    }

    public List<Stmt> Statements { get; }

    public override TResult Accept<TResult>(IStmtVisitor<TResult> visitor)
        => visitor.VisitBlockStmt(this);
}
