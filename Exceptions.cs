class ParserException : Exception
{
    public int Line { get; }

    public ParserException(string message, int line) : base(message)
    {
        Line = line;
    }

    public override string ToString()
    {
        return $"{Colors.Red} Error at line {Line}: {Message}{Colors.White}";
    }
}

class LexerException: Exception
{
    public LexerException(string message) : base(message)
    {

    }

    public override string ToString()
    {
        return $"{Colors.Red} Error at line {Line}: {Message}{Colors.White}";
    }
}