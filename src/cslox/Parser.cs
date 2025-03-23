using cslox.AbstractSyntaxTree;

namespace cslox;

public class Parser
{
    private readonly List<Token> _tokens;
    private int _current;

    public Parser(List<Token> tokens)
    {
        _tokens = tokens;
    }

    public List<Stmt?> Parse()
    {
        List<Stmt?> statements = new();
        while (!IsAtEnd())
        {
            statements.Add(Declaration());
        }
        return statements;
    }

    private Stmt? Declaration()
    {
        try
        {
            if (Match(TokenType.Var)) return VarDeclaration();
            return Statement();
        }
        catch (ParserException e)
        {
            Synchronise();
            return null;
        }
    }

    private Stmt VarDeclaration()
    {
         Token name = Consume(TokenType.Identifier, "Expect variable name.");
         Expr? initializer = null;
         if (Match(TokenType.Equal))
         {
             initializer = Expression();
         }

         Consume(TokenType.Semicolon, "Expect ';' after variable declaration.");
         return new Var(name, initializer);
    }

    private Stmt Statement()
    {
        if (Match(TokenType.Print)) return PrintStatement();
        return ExpressionStatement();
    }

    private Stmt PrintStatement()
    {
        Expr value = Expression();
        Consume(TokenType.Semicolon, "Expect ';' after value.");
        return new Print(value);
    }

    private Stmt ExpressionStatement()
    {
        Expr expr = Expression();
        Consume(TokenType.Semicolon, "Expect ';' after expression.");
        return new Expression(expr);
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

        if (Match(TokenType.Identifier)) return new Variable(Previous());

        if (Match(TokenType.LeftParen))
        {
            Expr expr = Expression();
            Consume(TokenType.RightParen, "Expect ')' after expression.");
            return new Grouping(expr);
        }

        throw Error(Peek(), "Expect expression.");
    }

    private Token Consume(TokenType type, string message)
    {
        if (Check(type)) return Advance();

        throw Error(Peek(), message);
    }

    private ParserException Error(Token token, string message)
    {
        Lox.Error(token, message);
        return new ParserException(token, message);
    }

    private void Synchronise()
    {
        Advance();

        while (!IsAtEnd())
        {
            if (Previous().Type == TokenType.Semicolon) return;

            switch (Peek().Type)
            {
                case TokenType.Class:
                case TokenType.Fun:
                case TokenType.Var:
                case TokenType.For:
                case TokenType.If:
                case TokenType.While:
                case TokenType.Print:
                case TokenType.Return:
                    return;
            }

            Advance();
        }
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
