class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            throw new ArgumentException("Arguments can't be null. Please specify the file name");
        }

        // Read input file and split to rows
        FileReader reader = new();
        string text = reader.Read(args[0]);
        // ...
        Lexer lexer = new(text);
        // Shoud be returned list of tokens
        lexer.Tokenize();
        Console.WriteLine(lexer.Tokens);
        for (int i = 0; i < lexer.Tokens.Count; i++)
        {
            Console.WriteLine(lexer.Tokens[i]);

        }

        // lection 4-6
    }
}