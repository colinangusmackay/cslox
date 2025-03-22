// *****************************************
// ** AUTO GENERATED FILE - DO NOT MODIFY **
// *****************************************
//
// Hash: a4b104f28f73e07fb8cee5fe914d00246952b11c8ee9eb6b8bbbd2e742712563
// Date Created: 2025-03-22 20:06:28 UTC
// Last Updated: 2025-03-22 20:06:28 UTC
// --------------------------------------------------------------------------------

namespace cslox.AbstractSyntaxTree;

public class Expression : Stmt
{
    public Expression(Expr expression)
    {
        Expression = expression;
    }

    public Expr Expression { get; }

    public override TResult Accept<TResult>(IVisitor<TResult> visitor)
        => visitor.VisitExpressionStmt(this);
}
