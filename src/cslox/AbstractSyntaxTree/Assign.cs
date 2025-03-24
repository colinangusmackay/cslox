// *****************************************
// ** AUTO GENERATED FILE - DO NOT MODIFY **
// *****************************************
//
// Hash: 9483cffa5c72fe27bb885e44eb6bedc47c1a8d8a6c0e4faa29f92d908af2fd7a
// Date Created: 2025-03-24 21:39:42 UTC
// Last Updated: 2025-03-24 21:39:42 UTC
// --------------------------------------------------------------------------------

namespace cslox.AbstractSyntaxTree;

public class Assign : Expr
{
    public Assign(Token name, Expr value)
    {
        Name = name;
        Value = value;
    }

    public Token Name { get; }

    public Expr Value { get; }

    public override TResult Accept<TResult>(IExprVisitor<TResult> visitor)
        => visitor.VisitAssignExpr(this);
}
