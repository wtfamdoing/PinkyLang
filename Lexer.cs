using System.Text;

class Lexer
{
    private int curr = 0;
    private int start = 0;
    private int dbgCounter = 0;
    private int line = 1;
    private string source = "";
    public List<Token> Tokens = [];

    public Lexer(string source)
    {
        this.source = source;
    }

    private bool IsAtEnd() => curr >= source.Length;

    private char Advance()
    {
        char ch = source[curr];
        curr++;
        return ch;
    }

    private char Peek()
    {
        if (IsAtEnd())
            return '\0';
        return source[curr];
    }

    private char Lookahead()
    {
        if (IsAtEnd())
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

    private void HadnleNumbers(char c)
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
        } else
        {
            AddToken(TokenType.TOK_INTEGER, sb.ToString());
        }
    }

    private void HandleStrings(char c)
    {
        var sb = new StringBuilder();
        sb.Append(c);
        while (Peek() != c && !IsAtEnd())
        {
            sb.Append(Advance());
        }
        sb.Append(Advance());
        AddToken(TokenType.TOK_STRING, sb.ToString());
    }

    private void HandleComments(char c)
    {
        while (Peek() != '\n')
            Advance();
    }

    private void HandleIdentifier(char c)
    {
        var sb = new StringBuilder();
        sb.Append(c);
        while (char.IsLetterOrDigit(Peek()) || Peek() == '_')
        {
            sb.Append(Advance());
        }
        string finalTokenLexeme = sb.ToString();
        if (KeywordsTable.Keywords.ContainsKey(finalTokenLexeme))
        {
             AddToken(KeywordsTable.Keywords[finalTokenLexeme], finalTokenLexeme);
        } else
        {
            AddToken(TokenType.TOK_IDENTIFIER, finalTokenLexeme);
        }
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
                case '-':
                    {
                        if (Match('-'))
                        {
                            HandleComments(ch);
                        } else
                        {
                            AddToken(TokenType.TOK_MINUS, "-");
                        }
                        break;
                    } 
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
                        } else
                        {
                            string val = ch.ToString();
                            AddToken(OperatorsTable.Ops[val], val);
                        }
                        break;
                    }
                case char c when char.IsDigit(c):
                    {
                        HadnleNumbers(c);
                        break;
                    }
                case '"':
                    {
                        HandleStrings(ch);
                        break;
                    }
                case '\'':
                    {
                        HandleStrings(ch);
                        break;
                    }
                case char c when char.IsLetter(c) || c == '_':
                    {
                        HandleIdentifier(c);
                        break;
                    }
            }
        }
    }
}