// *****************************************
// ** AUTO GENERATED FILE - DO NOT MODIFY **
// *****************************************
//
// Hash: 851423de7afcb227e56fcf2d30599a7bfe2be7b0e47f0ddd90b48415ad82d6db
// Date Created: 2025-03-22 20:41:56 UTC
// Last Updated: 2025-03-27 21:54:40 UTC
// --------------------------------------------------------------------------------

namespace cslox.AbstractSyntaxTree;

public interface IStmtVisitor<TResult>
{
        TResult VisitBlockStmt(Block block);
        TResult VisitExpressionStmt(Expression expression);
        TResult VisitIfStmt(If @if);
        TResult VisitPrintStmt(Print print);
        TResult VisitVarStmt(Var @var);
        TResult VisitWhileStmt(While @while);
}
