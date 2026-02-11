abstract class Expr
{
    public abstract override string ToString();
}

abstract class Statements
{
    public abstract override string ToString();
}

sealed class NumberLiteral : Expr
{
    public bool IsFloat { get; }
    public string? Lexeme { get; }

    public NumberLiteral(bool isFloat, string lexeme)
    {
        Lexeme = lexeme;
        IsFloat = isFloat;
    }

    public override string ToString() => (IsFloat ? "Float" : "Integer") + $"[{Lexeme}]";
}

sealed class Grouping : Expr
{
    public Expr Expression { get; }

    public Grouping(Expr expression) => Expression = expression;

    public override string ToString() => $"Group({Expression})";
}

sealed class Unary : Expr
{
    public Token Operator { get; }
    public Expr Operand { get; }

    public Unary(Token op, Expr operand)
    {
        Operator = op;
        Operand = operand;
    }

    public override string ToString() => $"Unary operator [{Operator}{Operand}]";
}

sealed class Binary : Expr
{
    public Token Operator { get; }
    public Expr Left { get; }
    public Expr Right { get; }

    public Binary(Token op, Expr left, Expr right)
    {
        Operator = op;
        Left = left;
        Right = right;
    }

    public override string ToString() => $"Binary operator, expression is  [{Left} {Operator} {Right}]";
}