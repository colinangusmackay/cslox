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

    public List<Stmt> Parse()
    {
        List<Stmt> statements = new();
        while (!IsAtEnd())
        {
            var statement = Declaration();
            if (statement != null) statements.Add(statement);
        }
        return statements;
    }

    private Stmt? Declaration()
    {
        try
        {
            if (Match(TokenType.Fun)) return Function("function");
            if (Match(TokenType.Var)) return VarDeclaration();
            return Statement();
        }
        catch (ParserException)
        {
            Synchronise();
            return null;
        }
    }

    private Stmt Function(string kind)
    {
        Token name = Consume(TokenType.Identifier, $"Expect {kind} name.");
        Consume(TokenType.LeftParen, $"Expect '(' after {kind} name.");
        List<Token> parameters = new();
        if (!Check(TokenType.RightParen))
        {
            do
            {
                if (parameters.Count >= 255)
                {
                    Error(Peek(), "Can't have more than 255 parameters.");
                }
                parameters.Add(Consume(TokenType.Identifier, "Expect parameter name."));
            } while (Match(TokenType.Comma));
        }

        Consume(TokenType.RightParen, "Expect ')' after parameters.");

        Consume(TokenType.LeftBrace, $"Expect '{{' before {kind} body.");
        List<Stmt> body = Block();
        return new Function(name, parameters, body);
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
        if (Match(TokenType.For)) return ForStatement();
        if (Match(TokenType.If)) return IfStatement();
        if (Match(TokenType.Print)) return PrintStatement();
        if (Match(TokenType.Return)) return ReturnStatement();
        if (Match(TokenType.While)) return WhileStatement();
        if (Match(TokenType.LeftBrace)) return new Block(Block());
        return ExpressionStatement();
    }

    private Stmt ReturnStatement()
    {
        Token keyword = Previous();
        Expr? value = null;
        if (!Check(TokenType.Semicolon))
        {
            value = Expression();
        }

        Consume(TokenType.Semicolon, "Expect ';' after return value.");
        return new Return(keyword, value);
    }

    private Stmt ForStatement()
    {
        // for (initializer; condition; increment) body
        Consume(TokenType.LeftParen, "Expect '(' after 'for'.");

        Stmt? initializer = null;
        if (Match(TokenType.Semicolon))
        {
            // Do nothing.
        }
        else if (Match(TokenType.Var))
        {
            initializer = VarDeclaration();
        }
        else
        {
            initializer = ExpressionStatement();
        }

        Expr? condition = null;
        if (!Check(TokenType.Semicolon))
        {
            condition = Expression();
        }
        Consume(TokenType.Semicolon, "Expect ';' after loop condition.");

        Expr? increment = null;
        if (!Check(TokenType.RightParen))
        {
            increment = Expression();
        }
        Consume(TokenType.RightParen, "Expect ')' after for clauses.");

        Stmt body = Statement();

        if (increment != null)
            body = new Block([body, new Expression(increment)]);

        if (condition == null) condition = new Literal(true);
        body = new While(condition, body);

        if (initializer != null)
        {
            body = new Block([initializer, body]);
        }

        return body;
    }

    private Stmt WhileStatement()
    {
        // while (condition) body
        Consume(TokenType.LeftParen, "Expect '(' after 'while'.");
        Expr condition = Expression();
        Consume(TokenType.RightParen, "Expect ')' after condition.");
        Stmt body = Statement();
        return new While(condition, body);
    }

    private Stmt IfStatement()
    {
        Consume(TokenType.LeftParen, "Expect '(' after 'if'.");
        Expr condition = Expression();
        Consume(TokenType.RightParen, "Expect ')' after if condition.");

        Stmt thenBranch = Statement();
        Stmt? elseBranch = null;
        if (Match(TokenType.Else)) elseBranch = Statement();

        return new If(condition, thenBranch, elseBranch);
    }

    private List<Stmt> Block()
    {
        List<Stmt> statements = new();
        while (!Check(TokenType.RightBrace) && !IsAtEnd())
        {
            statements.Add(Declaration()!);
        }

        Consume(TokenType.RightBrace, "Expect '}' after block.");
        return statements;
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
        => Assignment();

    private Expr Assignment()
    {
        Expr expr = Or();

        if (Match(TokenType.Equal))
        {
            Token equals = Previous();
            Expr value = Assignment();

            if (expr is Variable variable)
            {
                return new Assign(variable.Name, value);
            }

            Error(equals, "Invalid assignment target.");
        }

        return expr;
    }

    private Expr Or()
    {
        Expr expr = And();

        while (Match(TokenType.Or))
        {
            Token @operator = Previous();
            Expr right = And();
            expr = new Logical(expr, @operator, right);
        }

        return expr;
    }

    private Expr And()
    {
        Expr expr = Equality();

        while (Match(TokenType.And))
        {
            Token @operator = Previous();
            Expr right = Equality();
            expr = new Logical(expr, @operator, right);
        }

        return expr;
    }

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

        return Call();
    }

    private Expr Call()
    {
        Expr expr = Primary();

        while (true)
        {
            if (Match(TokenType.LeftParen))
            {
                expr = FinishCall(expr);
            }
            else
            {
                break;
            }
        }

        return expr;
    }

    private Expr FinishCall(Expr callee)
    {
        List<Expr> arguments = new List<Expr>();
        if (!Check(TokenType.RightParen))
        {
            do
            {
                if (arguments.Count >= 255)
                {
                    Error(Peek(), "Can't have more than 255 arguments.");
                }
                arguments.Add(Expression());
            } while (Match(TokenType.Comma));
        }

        Token paren = Consume(TokenType.RightParen, "Expect ')' after arguments.");
        return new Call(callee, paren, arguments);
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
