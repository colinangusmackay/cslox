// *****************************************
// ** AUTO GENERATED FILE - DO NOT MODIFY **
// *****************************************
//
// Hash: d6e860a639aafe2c7a2540c52cbfe95df9795642a6a7408fb4048e7eaa1ac0db
// Date Created: 2025-03-19 18:53:15 UTC
// Last Updated: 2025-03-22 20:06:28 UTC
// --------------------------------------------------------------------------------

namespace cslox.AbstractSyntaxTree;

public interface IVisitor<TResult>
{
        TResult VisitExpressionStmt(Expression expression);
        TResult VisitPrintStmt(Print print);
}
