using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

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

    private bool Except(TokenType type)
    {
        return false;
    }

    private bool Match(TokenType type)
    {
        if (type == _tokens[curr].Type)
        {
            return true;
        }
        return false;
    }

    private Token PreviousToken()
    {
        return _tokens[curr - 1];
    }

    public Expr ParsePrimary()
    {
        if (Match(TokenType.TOK_INTEGER)) return new NumberLiteral(false, PreviousToken().Lexeme);
        if (Match(TokenType.TOK_FLOAT)) return new NumberLiteral(true, PreviosToken().Lexeme);
        if (Match(TokenType.TOK_LPAREN))
        {
            Expr expr = ParseExpr();
            if (!Match(TokenType.TOK_RPAREN)) throw new Exception("The ')' is expected");
            else return new Grouping(expr);
        }
        throw new Exception($"Unexpected token");
    }

    public Expr ParseUnary()
    {
        if (Match(TokenType.TOK_NOT) || Match(TokenType.TOK_MINUS) || Match(TokenType.TOK_PLUS))
        {
            TokenType op = PreviousToken();
        }
        return ParsePrimary();
    }

    public Expr ParseFactor()
    {
        return ParseUnary();
    }

    public void ParseTerm()
    {

    }

    public void ParseExpr()
    {

    }

    public void Parse()
    {

    }
}