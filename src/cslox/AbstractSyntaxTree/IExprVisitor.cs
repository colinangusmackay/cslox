// *****************************************
// ** AUTO GENERATED FILE - DO NOT MODIFY **
// *****************************************
//
// Hash: 24b0352b21469d49c443edb671c2392b8f78db0c1ee5df2033991652fe577cc4
// Date Created: 2025-03-19 18:53:15 UTC
// Last Updated: 2025-03-30 19:03:52 UTC
// --------------------------------------------------------------------------------

namespace cslox.AbstractSyntaxTree;

public interface IExprVisitor<TResult>
{
        TResult VisitAssignExpr(Assign assign);
        TResult VisitBinaryExpr(Binary binary);
        TResult VisitCallExpr(Call call);
        TResult VisitGroupingExpr(Grouping grouping);
        TResult VisitLiteralExpr(Literal literal);
        TResult VisitLogicalExpr(Logical logical);
        TResult VisitUnaryExpr(Unary unary);
        TResult VisitVariableExpr(Variable variable);
}
