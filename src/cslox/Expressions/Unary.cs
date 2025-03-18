// AUTO GENERATED FILE

namespace cslox.Expressions;

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
