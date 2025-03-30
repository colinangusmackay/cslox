// *****************************************
// ** AUTO GENERATED FILE - DO NOT MODIFY **
// *****************************************
//
// Hash: 4f486e6bc56559b1a3bcd50c3823ebd4cbbdd858fbfc30c47ed5c8df4bdc1d7f
// Date Created: 2025-03-30 21:23:31 UTC
// Last Updated: 2025-03-30 21:23:31 UTC
// --------------------------------------------------------------------------------

namespace cslox.AbstractSyntaxTree;

public class Function : Stmt
{
    public Function(Token name, List<Token> parameters, List<Stmt> body)
    {
        Name = name;
        Parameters = parameters;
        Body = body;
    }

    public Token Name { get; }

    public List<Token> Parameters { get; }

    public List<Stmt> Body { get; }

    public override TResult Accept<TResult>(IStmtVisitor<TResult> visitor)
        => visitor.VisitFunctionStmt(this);
}
