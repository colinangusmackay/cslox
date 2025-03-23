// *****************************************
// ** AUTO GENERATED FILE - DO NOT MODIFY **
// *****************************************
//
// Hash: 4cdfd7919d49f95b5a0794f9188e48e7c5c7ba071ceddab725eab6e30417d961
// Date Created: 2025-03-23 21:32:33 UTC
// Last Updated: 2025-03-23 21:32:33 UTC
// --------------------------------------------------------------------------------

namespace cslox.AbstractSyntaxTree;

public class Var : Stmt
{
    public Var(Token name, Expr? initializer)
    {
        Name = name;
        Initializer = initializer;
    }

    public Token Name { get; }

    public Expr? Initializer { get; }

    public override TResult Accept<TResult>(IStmtVisitor<TResult> visitor)
        => visitor.VisitVarStmt(this);
}
