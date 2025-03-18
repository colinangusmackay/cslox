// AUTO GENERATED FILE

namespace cslox;

public abstract class Expr
{

    public class Binary : Expr
    {
        public Binary(Expr left, Token operator, Expr right)
        {
            Left = left;
            Operator = operator;
            Right = right;
        }

        public Expr Left { get; }

        public Token Operator { get; }

        public Expr Right { get; }
    }

    public class Grouping : Expr
    {
        public Grouping(Expr expression)
        {
            Expression = expression;
        }

        public Expr Expression { get; }
    }

    public class Literal : Expr
    {
        public Literal(object value)
        {
            Value = value;
        }

        public object Value { get; }
    }

    public class Unary : Expr
    {
        public Unary(Token operator, Expr right)
        {
            Operator = operator;
            Right = right;
        }

        public Token Operator { get; }

        public Expr Right { get; }
    }
}
