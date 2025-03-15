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
        foreach(var token in tokens)
        {
            Console.WriteLine(token);
        }
    }

    internal static void Error(int line, string message)
    {
        Report(line, "", message);
    }

    private static void Report(int line, string where, string message)
    {
        Console.WriteLine($"[{line}] Error{where}: {message}");
    }
}