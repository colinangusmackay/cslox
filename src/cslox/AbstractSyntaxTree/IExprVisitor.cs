// *****************************************
// ** AUTO GENERATED FILE - DO NOT MODIFY **
// *****************************************
//
// Hash: b6d5fb83ad829619b4a7d3db9b592d0937d015a383033cf361cd90c4ea30addd
// Date Created: 2025-03-19 18:53:15 UTC
// Last Updated: 2025-03-23 21:32:33 UTC
// --------------------------------------------------------------------------------

namespace cslox.AbstractSyntaxTree;

public interface IExprVisitor<TResult>
{
        TResult VisitBinaryExpr(Binary binary);
        TResult VisitGroupingExpr(Grouping grouping);
        TResult VisitLiteralExpr(Literal literal);
        TResult VisitUnaryExpr(Unary unary);
        TResult VisitVariableExpr(Variable variable);
}
