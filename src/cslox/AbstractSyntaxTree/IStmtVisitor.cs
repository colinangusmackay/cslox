// *****************************************
// ** AUTO GENERATED FILE - DO NOT MODIFY **
// *****************************************
//
// Hash: 8347b8a0d1b167b47d2776f474ede481b07c3620be0917a638e0ec4b06ce69d5
// Date Created: 2025-03-22 20:41:56 UTC
// Last Updated: 2025-03-26 21:31:09 UTC
// --------------------------------------------------------------------------------

namespace cslox.AbstractSyntaxTree;

public interface IStmtVisitor<TResult>
{
        TResult VisitBlockStmt(Block block);
        TResult VisitExpressionStmt(Expression expression);
        TResult VisitIfStmt(If @if);
        TResult VisitPrintStmt(Print print);
        TResult VisitVarStmt(Var @var);
}
