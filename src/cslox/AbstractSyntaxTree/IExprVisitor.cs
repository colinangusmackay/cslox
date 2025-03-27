// *****************************************
// ** AUTO GENERATED FILE - DO NOT MODIFY **
// *****************************************
//
// Hash: 8394e9313641b559547f2f1e0acad3114a053e5e3e3bb71624f5c9584245d715
// Date Created: 2025-03-19 18:53:15 UTC
// Last Updated: 2025-03-27 20:44:42 UTC
// --------------------------------------------------------------------------------

namespace cslox.AbstractSyntaxTree;

public interface IExprVisitor<TResult>
{
        TResult VisitAssignExpr(Assign assign);
        TResult VisitBinaryExpr(Binary binary);
        TResult VisitGroupingExpr(Grouping grouping);
        TResult VisitLiteralExpr(Literal literal);
        TResult VisitLogicalExpr(Logical logical);
        TResult VisitUnaryExpr(Unary unary);
        TResult VisitVariableExpr(Variable variable);
}
