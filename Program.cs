class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            throw new ArgumentException("Arguments can't be null. Please specify the file name");
        }

        string text = FileReader.Read(args[0]);
        Lexer lexer = new(text);
        lexer.Tokenize();
        Console.WriteLine(lexer.Tokens);
        for (int i = 0; i < lexer.Tokens.Count; i++)
        {
            Console.WriteLine(lexer.Tokens[i]);
        }
    }
}