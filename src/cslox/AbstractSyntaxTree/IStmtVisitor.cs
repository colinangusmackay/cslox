// *****************************************
// ** AUTO GENERATED FILE - DO NOT MODIFY **
// *****************************************
//
// Hash: ba2b9699b26ac0b16cf828fb91e20d41b812f5382fafdc8d069e8dd44a8df37c
// Date Created: 2025-03-22 20:41:56 UTC
// Last Updated: 2025-03-31 20:52:28 UTC
// --------------------------------------------------------------------------------

namespace cslox.AbstractSyntaxTree;

public interface IStmtVisitor<TResult>
{
        TResult VisitBlockStmt(Block block);
        TResult VisitExpressionStmt(Expression expression);
        TResult VisitFunctionStmt(Function function);
        TResult VisitIfStmt(If @if);
        TResult VisitPrintStmt(Print print);
        TResult VisitReturnStmt(Return @return);
        TResult VisitVarStmt(Var @var);
        TResult VisitWhileStmt(While @while);
}
