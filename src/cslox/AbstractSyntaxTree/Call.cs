// *****************************************
// ** AUTO GENERATED FILE - DO NOT MODIFY **
// *****************************************
//
// Hash: e669b5dd3a3591ca37a779839275c6d93ec5432ee3ad58a5ab079ec760821754
// Date Created: 2025-03-30 19:03:52 UTC
// Last Updated: 2025-03-30 19:03:52 UTC
// --------------------------------------------------------------------------------

namespace cslox.AbstractSyntaxTree;

public class Call : Expr
{
    public Call(Expr callee, Token paren, List<Expr> arguments)
    {
        Callee = callee;
        Paren = paren;
        Arguments = arguments;
    }

    public Expr Callee { get; }

    public Token Paren { get; }

    public List<Expr> Arguments { get; }

    public override TResult Accept<TResult>(IExprVisitor<TResult> visitor)
        => visitor.VisitCallExpr(this);
}
