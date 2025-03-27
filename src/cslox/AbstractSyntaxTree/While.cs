// *****************************************
// ** AUTO GENERATED FILE - DO NOT MODIFY **
// *****************************************
//
// Hash: a42c14338b9e1da8895f8ec77e1e74667f9062996f5cb6376977d3ca0b4e4b9f
// Date Created: 2025-03-27 21:01:52 UTC
// Last Updated: 2025-03-27 21:01:52 UTC
// --------------------------------------------------------------------------------

namespace cslox.AbstractSyntaxTree;

public class While : Stmt
{
    public While(Expr condition, Stmt body)
    {
        Condition = condition;
        Body = body;
    }

    public Expr Condition { get; }

    public Stmt Body { get; }

    public override TResult Accept<TResult>(IStmtVisitor<TResult> visitor)
        => visitor.VisitWhileStmt(this);
}
