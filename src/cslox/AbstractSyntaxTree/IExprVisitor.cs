// *****************************************
// ** AUTO GENERATED FILE - DO NOT MODIFY **
// *****************************************
//
// Hash: 03076a62c41528da4f933f0b29e0971511d34636b647be3e4a1940458657ba3f
// Date Created: 2025-03-19 18:53:15 UTC
// Last Updated: 2025-03-24 21:39:42 UTC
// --------------------------------------------------------------------------------

namespace cslox.AbstractSyntaxTree;

public interface IExprVisitor<TResult>
{
        TResult VisitAssignExpr(Assign assign);
        TResult VisitBinaryExpr(Binary binary);
        TResult VisitGroupingExpr(Grouping grouping);
        TResult VisitLiteralExpr(Literal literal);
        TResult VisitUnaryExpr(Unary unary);
        TResult VisitVariableExpr(Variable variable);
}
