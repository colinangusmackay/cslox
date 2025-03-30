// *****************************************
// ** AUTO GENERATED FILE - DO NOT MODIFY **
// *****************************************
//
// Hash: acef9139a974f320ca77c92da6a66253726cc394128e98e9c50afc27d583cac9
// Date Created: 2025-03-22 20:41:56 UTC
// Last Updated: 2025-03-30 21:23:31 UTC
// --------------------------------------------------------------------------------

namespace cslox.AbstractSyntaxTree;

public interface IStmtVisitor<TResult>
{
        TResult VisitBlockStmt(Block block);
        TResult VisitExpressionStmt(Expression expression);
        TResult VisitFunctionStmt(Function function);
        TResult VisitIfStmt(If @if);
        TResult VisitPrintStmt(Print print);
        TResult VisitVarStmt(Var @var);
        TResult VisitWhileStmt(While @while);
}
