// *****************************************
// ** AUTO GENERATED FILE - DO NOT MODIFY **
// *****************************************
//
// Hash: d95ca63f341f5f5ad6a82861f2c8b478e9cdc8df6dded3c7c6f1cab4d799cc2c
// Date Created: 2025-03-31 20:49:04 UTC
// Last Updated: 2025-03-31 21:30:29 UTC
// --------------------------------------------------------------------------------

namespace cslox.AbstractSyntaxTree;

public class Return : Stmt
{
    public Return(Token keyword, Expr? value)
    {
        Keyword = keyword;
        Value = value;
    }

    public Token Keyword { get; }

    public Expr? Value { get; }

    public override TResult Accept<TResult>(IStmtVisitor<TResult> visitor)
        => visitor.VisitReturnStmt(this);
}
