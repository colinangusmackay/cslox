using cslox.Expressions;

namespace cslox;

public class Lox
{
    internal static bool HadError { get; set; } = false;

    internal static void RunPrompt()
    {
        Console.WriteLine("cslox REPL. Enter a blank line to exit.");
        while (true)
        {
            Console.Write("> ");
            var line = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(line)) break;
            Run(line);
            HadError = false;
        }
    }

    internal static async Task RunFileAsync(string filePath)
    {
        var content = await File.ReadAllTextAsync(filePath);
        Run(content);
        if (HadError) Environment.Exit((int)SysExits.DataError);
    }

    private static void Run(string source)
    {
        Scanner scanner = new Scanner(source);
        List<Token> tokens = scanner.ScanTokens();
        Console.WriteLine("Tokens:");
        foreach(var token in tokens)
        {
            Console.WriteLine(token);
        }

        Console.WriteLine();
        Parser parser = new Parser(tokens);
        Expr? expression = parser.Parse();
        if (expression == null) return;
        Console.WriteLine("AST:");
        Console.WriteLine(new AstPrinter().Print(expression));
    }

    internal static void Error(int line, string message)
    {
        Report(line, "", message);
    }

    internal static void Error(Token token, string message)
    {
        Report(
            token.Line,
            token.Type == TokenType.Eof
                ? " at end"
                : $" at '{token.Lexeme}'",
            message);
    }

    private static void Report(int line, string where, string message)
    {
        Console.WriteLine($"[{line}] Error{where}: {message}");
    }
}
