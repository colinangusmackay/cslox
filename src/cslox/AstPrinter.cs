using System.Text;
using cslox.AbstractSyntaxTree;

namespace cslox;

public class AstPrinter : IVisitor<string>
{
    public string Print(Expr expr)
        => expr.Accept(this);

    public string VisitBinaryExpr(Binary binary)
        => Parenthesise(binary.Operator.Lexeme, binary.Left, binary.Right);

    public string VisitGroupingExpr(Grouping grouping)
        => Parenthesise("group", grouping.Expression);

    public string VisitLiteralExpr(Literal literal)
        => literal.Value?.ToString() ?? "nil";

    public string VisitUnaryExpr(Unary unary)
        => Parenthesise(unary.Operator.Lexeme, unary.Right);

    private string Parenthesise(string op, params Expr[] exprs)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append('(');
        sb.Append(op);
        foreach (var expr in exprs)
        {
            sb.Append(' ');
            sb.Append(expr.Accept(this));
        }

        sb.Append(')');
        return sb.ToString();
    }
}
