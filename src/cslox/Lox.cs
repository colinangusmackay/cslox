using cslox.AbstractSyntaxTree;

namespace cslox;

public class Lox
{
    private static readonly Interpreter Interpreter = new();
    internal static bool HadError { get; set; } = false;
    internal static bool HadRuntimeError { get; set; } = false;

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
        if (HadRuntimeError) Environment.Exit((int)SysExits.Software);
    }

    private static void Run(string source)
    {
        Scanner scanner = new Scanner(source);
        List<Token> tokens = scanner.ScanTokens();
        Parser parser = new Parser(tokens);
        Expr? expression = parser.Parse();
        if (expression == null) return;
        Interpreter.Interpret(expression);
    }

    internal static void RuntimeError(RuntimeException e)
    {
        Console.WriteLine(e.Message);
        Console.WriteLine($"[{e.Token.Line}] Error at '{e.Token.Lexeme}'");
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
