// *****************************************
// ** AUTO GENERATED FILE - DO NOT MODIFY **
// *****************************************
//
// Hash: 107aec59cb6b539d6e96780d6e4536a07ee6e96569f784480033935f4c946174
// Date Created: 2025-03-22 20:41:56 UTC
// Last Updated: 2025-03-25 22:50:37 UTC
// --------------------------------------------------------------------------------

namespace cslox.AbstractSyntaxTree;

public interface IStmtVisitor<TResult>
{
        TResult VisitBlockStmt(Block block);
        TResult VisitExpressionStmt(Expression expression);
        TResult VisitPrintStmt(Print print);
        TResult VisitVarStmt(Var var);
}
