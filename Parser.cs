using System.Reflection.Emit;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization.Infrastructure;

class Parser
{
    private int curr = 0;
    private List<Token> _tokens;

    public Parser(List<Token> tokens)
    {
        _tokens = tokens;
    }

    private bool IsAtEnd() => curr >= _tokens.Count;

    private Token Advance()
    {
        Token token = _tokens[curr];
        curr++;
        return token;
    }

    private Token Peek()
    {
        return _tokens[curr];
    }

    private bool isNext(TokenType type)
    {
        if (IsAtEnd()) return false;
        return type == Peek().Type;
    }

    private Token Expect(TokenType type)
    {
        if (IsAtEnd()) throw new Exception($"Found a {PreviousToken().Lexeme} at the end of the string");
        if (type != Peek().Type) throw new Exception($"Token type doesn't match expected. Expected is '{type}', actual is '{Peek().Type}'");
        return Advance();
    }

    private bool Match(TokenType type)
    {
        if(IsAtEnd()) return false;
        if (type != Peek().Type) return false;
        curr++;
        return true;
    }

    private Token PreviousToken()
    {
        if (curr > 0) return _tokens[curr - 1];

        throw new Exception("Couldn't get a previos token, because the current position is 0");
    }

    public Expr ParsePrimary()
    {
        if (Match(TokenType.TOK_INTEGER)) return new NumberLiteral(false, PreviousToken().Lexeme);
        if (Match(TokenType.TOK_FLOAT)) return new NumberLiteral(true, PreviousToken().Lexeme);
        if (Match(TokenType.TOK_LPAREN))
        {
            Expr expr = ParseExpr();
            if (!Match(TokenType.TOK_RPAREN)) throw new Exception("The ')' is expected");
            else return new Grouping(expr);
        }
        throw new Exception($"Unexpected token. Current token is {_tokens[curr]}");
    }

    public Expr ParseUnary()
    {
        if (Match(TokenType.TOK_NOT) || Match(TokenType.TOK_MINUS) || Match(TokenType.TOK_PLUS))
        {
            Token op = PreviousToken();
            Expr operand = ParseUnary();
            return new Unary(op, operand);
        }
        return ParsePrimary();
    }

    public Expr ParseFactor()
    {
        return ParseUnary();
    }

    public Expr ParseTerm()
    {
        Expr expr = ParseFactor();
        while (Match(TokenType.TOK_STAR) || Match(TokenType.TOK_SLASH))
        {
            Token op = PreviousToken();
            Expr right = ParseFactor();
            expr = new Binary(op, expr, right);
        }
        return expr;
    }

    public Expr ParseExpr()
    {
        Expr expr = ParseTerm();
        while (Match(TokenType.TOK_PLUS) || Match(TokenType.TOK_MINUS))
        {
            Token op = PreviousToken();
            Expr right = ParseTerm();
            expr = new Binary(op, expr, right);
        }
        return expr;
    }

    public Expr Parse()
    {
        Expr ast = ParseExpr();
        return ast;
    }
}