// *****************************************
// ** AUTO GENERATED FILE - DO NOT MODIFY **
// *****************************************
//
// Hash: 0130bfbd11829dea101d395341f02720785c860409fa04bd721a757b12e95620
// Date Created: 2025-03-22 20:06:28 UTC
// Last Updated: 2025-03-22 20:45:57 UTC
// --------------------------------------------------------------------------------

namespace cslox.AbstractSyntaxTree;

public abstract class Stmt
{
    public abstract TResult Accept<TResult>(IStmtVisitor<TResult> visitor);
}
