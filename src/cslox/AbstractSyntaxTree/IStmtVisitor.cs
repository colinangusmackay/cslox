// *****************************************
// ** AUTO GENERATED FILE - DO NOT MODIFY **
// *****************************************
//
// Hash: bc44909296cd89d3d819dec60272c96fc803520accfbf422f131bd9711ed1687
// Date Created: 2025-03-22 20:41:56 UTC
// Last Updated: 2025-03-22 21:20:36 UTC
// --------------------------------------------------------------------------------

namespace cslox.AbstractSyntaxTree;

public interface IStmtVisitor
{
        void VisitExpressionStmt(Expression expression);
        void VisitPrintStmt(Print print);
}

public interface IStmtVisitor<TResult>
{
        TResult VisitExpressionStmt(Expression expression);
        TResult VisitPrintStmt(Print print);
}
