class Token(TokenType type, string? lexeme, int line)
{
    public TokenType Type { get; } = type;
    public string? Lexeme { get; } = lexeme;
    public int Line { get; } = line;

    public override string ToString()
    {
        return $"Token >> Token Type: {Type}, Token Lexeme: {Lexeme} at the line {Line}";
    }
}