// *****************************************
// ** AUTO GENERATED FILE - DO NOT MODIFY **
// *****************************************
//
// Hash: 7b353766fcec27fd545508e152eac8097e8d2c66dd68d738a52b2e1defd39ea2
// Date Created: 2025-03-19 18:53:15 UTC
// Last Updated: 2025-03-22 21:20:36 UTC
// --------------------------------------------------------------------------------

namespace cslox.AbstractSyntaxTree;

public interface IExprVisitor
{
        void VisitBinaryExpr(Binary binary);
        void VisitGroupingExpr(Grouping grouping);
        void VisitLiteralExpr(Literal literal);
        void VisitUnaryExpr(Unary unary);
}

public interface IExprVisitor<TResult>
{
        TResult VisitBinaryExpr(Binary binary);
        TResult VisitGroupingExpr(Grouping grouping);
        TResult VisitLiteralExpr(Literal literal);
        TResult VisitUnaryExpr(Unary unary);
}
