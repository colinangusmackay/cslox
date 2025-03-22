// *****************************************
// ** AUTO GENERATED FILE - DO NOT MODIFY **
// *****************************************
//
// Hash: 9f7cfe84ee765e8cf6bd8b6edb14b6cba3e2ebc5045700f6dcfef837626dda9c
// Date Created: 2025-03-19 18:53:15 UTC
// Last Updated: 2025-03-22 20:41:56 UTC
// --------------------------------------------------------------------------------

namespace cslox.AbstractSyntaxTree;

public interface IExprVisitor<TResult>
{
        TResult VisitBinaryExpr(Binary binary);
        TResult VisitGroupingExpr(Grouping grouping);
        TResult VisitLiteralExpr(Literal literal);
        TResult VisitUnaryExpr(Unary unary);
}
