// *****************************************
// ** AUTO GENERATED FILE - DO NOT MODIFY **
// *****************************************
//
// Hash: 7f24a5c717ff92e99ba9e24c47ce86950cd5037936091a9dca38fdddb324c65b
// Date Created: 2025-03-26 21:12:28 UTC
// Last Updated: 2025-03-26 21:31:09 UTC
// --------------------------------------------------------------------------------

namespace cslox.AbstractSyntaxTree;

public class If : Stmt
{
    public If(Expr condition, Stmt thenBranch, Stmt? elseBranch)
    {
        Condition = condition;
        ThenBranch = thenBranch;
        ElseBranch = elseBranch;
    }

    public Expr Condition { get; }

    public Stmt ThenBranch { get; }

    public Stmt? ElseBranch { get; }

    public override TResult Accept<TResult>(IStmtVisitor<TResult> visitor)
        => visitor.VisitIfStmt(this);
}
