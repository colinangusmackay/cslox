// *****************************************
// ** AUTO GENERATED FILE - DO NOT MODIFY **
// *****************************************
//
// Hash: 3c88bcd15daf395bc22f9d6107537635c68cd08cc43d13e98da89c2387d2a01c
// Date Created: 2025-03-31 20:49:04 UTC
// Last Updated: 2025-03-31 20:51:23 UTC
// --------------------------------------------------------------------------------

namespace cslox.AbstractSyntaxTree;

public class Return : Stmt
{
    public Return(Token keyword, Expr? value)
    {
        keyword = keyword;
        Value = value;
    }

    public Token keyword { get; }

    public Expr? Value { get; }

    public override TResult Accept<TResult>(IStmtVisitor<TResult> visitor)
        => visitor.VisitReturnStmt(this);
}
