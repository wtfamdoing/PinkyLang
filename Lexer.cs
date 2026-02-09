using System.Text;
using System.Diagnostics;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

class Lexer
{
    private int curr = 0;
    private int start = 0;
    private int line = 1;
    private string source = "";
    public List<Token> Tokens = [];

    public Lexer(string source)
    {
        this.source = source;
    }

    private bool isAtEnd() => curr >= source.Length;

    private char Advance()
    {
        char ch = source[curr];
        curr++;
        return ch;
    }

    private char Peek()
    {
        if (isAtEnd())
            return '\0';
        return source[curr];
    }

    private char Lookahead()
    {
        if (isAtEnd())
        {
            return '\0';
        }
        return source[curr + 1];
    }

    private bool Match(char expected)
    {
        if (curr > source.Length)
        {
            return false;
        }
        if (source[curr] != expected)
        {
            return false;
        }
        curr++;
        return true;
    }

    private void AddToken(TokenType type, string lexeme)
    {
        Tokens.Add(new Token(type, lexeme, line));
    }
    public void Tokenize()
    {
        while (curr < source.Length)
        {
            start = curr;
            char ch = Advance();
            switch (ch)
            {
                case '\n': line++;                                break;
                case ' ':
                case '\t':
                case '\r':                                        break;
                case '+': AddToken(TokenType.TOK_PLUS, "+");      break;
                case '-': AddToken(TokenType.TOK_MINUS, "-");     break;
                case '*': AddToken(TokenType.TOK_STAR, "*");      break;
                case '/': AddToken(TokenType.TOK_SLASH, "/");     break;
                case '^': AddToken(TokenType.TOK_CARET, "^");     break;
                case '%': AddToken(TokenType.TOK_MOD, "%");       break;
                case '?': AddToken(TokenType.TOK_QUESTION, "?");  break;
                case ';': AddToken(TokenType.TOK_SEMICOLON, ";"); break;
                case '(': AddToken(TokenType.TOK_LPAREN, "(");    break;
                case ')': AddToken(TokenType.TOK_RPAREN, ")");    break;
                case '[': AddToken(TokenType.TOK_LSQUAR, "[");    break;
                case ']': AddToken(TokenType.TOK_RSQUAR, "]");    break;
                case '{': AddToken(TokenType.TOK_LCURLY, "{");    break;
                case '}': AddToken(TokenType.TOK_RCURLY, "}");    break;
                case '.': AddToken(TokenType.TOK_DOT, ".");       break;
                case ',': AddToken(TokenType.TOK_COMMA, ",");     break;
                case '#':
                    {
                        while(Peek() != '\n')
                        {
                            Advance();
                        }
                        break;
                    }
                case '=':
                case '>':
                case '<':
                case '~':
                case ':':
                    {
                        if (Match('='))
                        {
                            AddToken(OperatorsTable.Ops[$"{ch}="], $"{ch}=");
                        }
                        break;
                    }
                case char c when char.IsDigit(c):
                    {
                        var sb = new StringBuilder();
                        sb.Append(c);
                        while (char.IsDigit(Peek()))
                            sb.Append(Advance());
                        if (Peek() == '.' && char.IsDigit(Lookahead()))
                        {
                            sb.Append(Advance());
                            while (char.IsDigit(Peek()))
                                sb.Append(Advance());
                            AddToken(TokenType.TOK_FLOAT, sb.ToString());
                        }
                        else
                        {
                            AddToken(TokenType.TOK_INTEGER, sb.ToString());
                        }
                        break;
                    }
            }
        }
    }
}