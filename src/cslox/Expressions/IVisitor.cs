// *****************************************
// ** AUTO GENERATED FILE - DO NOT MODIFY **
// *****************************************
//
// Hash: f551a693df16affe52640e214fe51dd2ce48d3412b0b29ca6aa1903c85c5d96c
// Date Created: 2025-03-19 18:53:15 UTC
// Last Updated: 2025-03-19 18:53:15 UTC
// --------------------------------------------------------------------------------

namespace cslox.Expressions;

public interface IVisitor<TResult>
{
        TResult VisitBinaryExpr(Binary binary);
        TResult VisitGroupingExpr(Grouping grouping);
        TResult VisitLiteralExpr(Literal literal);
        TResult VisitUnaryExpr(Unary unary);
}
