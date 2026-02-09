public enum TokenType
    {
        // Keywords
        TOK_IF,           // if
        TOK_THEN,        // then
        TOK_ELSE,        // else
        TOK_TRUE,        // true
        TOK_FALSE,       // false
        TOK_AND,         // and
        TOK_OR,          // or
        TOK_WHILE,       // while
        TOK_DO,          // do
        TOK_FOR,         // for
        TOK_FUNC,        // func
        TOK_NULL,        // null
        TOK_END,         // end
        TOK_PRINT,       // print
        TOK_PRINTLN,     // println
        TOK_RET,         // ret

        // Symbols
        TOK_LPAREN,      // (
        TOK_RPAREN,      // )
        TOK_LCURLY,      // {
        TOK_RCURLY,      // }
        TOK_LSQUAR,      // [
        TOK_RSQUAR,      // ]
        TOK_COMMA,       // ,
        TOK_DOT,         // .
        TOK_PLUS,        // +
        TOK_MINUS,       // -
        TOK_STAR,        // *
        TOK_SLASH,       // /
        TOK_CARET,       // ^
        TOK_MOD,         // %
        TOK_COLON,       // :
        TOK_SEMICOLON,   // ;
        TOK_QUESTION,    // ?
        TOK_NOT,         // ~
        TOK_GT,          // >
        TOK_LT,          // <
        TOK_GE,          // >=
        TOK_LE,          // <=
        TOK_NE,          // ~=
        TOK_EQ,          // ==
        TOK_ASSIGN,      // :=
        TOK_GTGT,        // >>
        TOK_LTLT,        // <<


        // Literals
        TOK_IDENTIFIER,
        TOK_STRING,
        TOK_INTEGER,
        TOK_FLOAT
    }

public static class OperatorsTable
{
    public static readonly IReadOnlyDictionary<string, TokenType> Ops = new Dictionary<string, TokenType>
    {
        ["=="] = TokenType.TOK_EQ,
        [">"] = TokenType.TOK_GT,
        [">="] = TokenType.TOK_GE,
        [">>"] = TokenType.TOK_GTGT,
        ["<"] = TokenType.TOK_LT,
        ["<="] = TokenType.TOK_LE,
        ["<<"] = TokenType.TOK_LTLT,
        ["~"] = TokenType.TOK_NOT,
        ["~="] = TokenType.TOK_NE,
        [":"] = TokenType.TOK_COLON,
        [":="] = TokenType.TOK_ASSIGN
    };
}