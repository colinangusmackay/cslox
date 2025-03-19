using System.ComponentModel.Design;
using cslox.Expressions;

namespace cslox;

public class Parser
{
    private readonly List<Token> _tokens = new();
    private int _current = 0;

    public Parser(List<Token> tokens)
    {
        _tokens = tokens;
    }

    private Expr Expression()
        => Equality();

    private Expr Equality()
        => LeftAssociative(Comparison, TokenType.BangEqual, TokenType.EqualEqual);

    private Expr Comparison()
        => LeftAssociative(Term, TokenType.Greater, TokenType.GreaterEqual, TokenType.Less, TokenType.LessEqual);

    private Expr Term()
        => LeftAssociative(Factor, TokenType.Minus, TokenType.Plus);

    private Expr Factor()
        => LeftAssociative(Unary, TokenType.Slash, TokenType.Star);

    private Expr LeftAssociative(Func<Expr> nextLevel, params Span<TokenType> operators)
    {
        Expr expr = nextLevel();

        while (Match(operators))
        {
            Token @operator = Previous();
            Expr right = nextLevel();
            expr = new Binary(expr, @operator, right);
        }

        return expr;
    }

    private Expr Unary()
    {
        if (Match(TokenType.Bang, TokenType.Minus))
        {
            Token @operator = Previous();
            Expr right = Unary();
            return new Unary(@operator, right);
        }

        return Primary();
    }

    private Expr Primary()
    {
        if (Match(TokenType.False)) return new Literal(false);
        if (Match(TokenType.True)) return new Literal(true);
        if (Match(TokenType.Nil)) return new Literal(null);
        if (Match(TokenType.Number, TokenType.String))
        {
            return new Literal(Previous().Literal);
        }

        if (Match(TokenType.LeftParen))
        {
            Expr expr = Expression();
            Consume(TokenType.RightParen, "Expect ')' after expression.");
            return new Grouping(expr);
        }

        throw new NotImplementedException();
    }

    private void Consume(TokenType rightParen, string expectAfterExpression)
    {
        throw new NotImplementedException();
    }

    private bool Match(params Span<TokenType> types)
    {
        foreach(var type in types)
        {
            if (Check(type))
            {
                Advance();
                return true;
            }
        }

        return false;
    }

    private bool Check(TokenType type)
    {
        if (IsAtEnd()) return false;
        return Peek().Type == type;
    }

    private Token Advance()
    {
        if (!IsAtEnd()) _current++;
        return Previous();
    }

    private bool IsAtEnd()
        => Peek().Type == TokenType.Eof;

    private Token Peek()
        => _tokens[_current];

    private Token Previous()
        => _tokens[_current - 1];
}
