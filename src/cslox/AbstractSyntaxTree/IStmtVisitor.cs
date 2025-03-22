// *****************************************
// ** AUTO GENERATED FILE - DO NOT MODIFY **
// *****************************************
//
// Hash: c1c48f1b3c24f8e46035524d1b4f66cd8e885ac5dfe3178cab99542300bb1922
// Date Created: 2025-03-22 20:41:56 UTC
// Last Updated: 2025-03-22 21:41:42 UTC
// --------------------------------------------------------------------------------

namespace cslox.AbstractSyntaxTree;

public interface IStmtVisitor<TResult>
{
        TResult VisitExpressionStmt(Expression expression);
        TResult VisitPrintStmt(Print print);
}
