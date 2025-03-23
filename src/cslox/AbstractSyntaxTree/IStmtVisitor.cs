// *****************************************
// ** AUTO GENERATED FILE - DO NOT MODIFY **
// *****************************************
//
// Hash: beca88398219d2a1685297fb4339bc4533d31422e6421e4c91a1717f8418dbbb
// Date Created: 2025-03-22 20:41:56 UTC
// Last Updated: 2025-03-23 21:32:33 UTC
// --------------------------------------------------------------------------------

namespace cslox.AbstractSyntaxTree;

public interface IStmtVisitor<TResult>
{
        TResult VisitExpressionStmt(Expression expression);
        TResult VisitPrintStmt(Print print);
        TResult VisitVarStmt(Var var);
}
