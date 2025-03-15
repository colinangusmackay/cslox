namespace cslox;

public class Program
{
    internal static bool HadError { get; set; } = false;

    public static async Task Main(string[] args)
    {
        if (args.Length > 1)
        {
            Console.WriteLine("Usage: cslox [script]");
            Environment.Exit((int)SysExits.Usage);
        }
        else if (args.Length == 1)
        {
            await RunFileAsync(args[0]);
        }
        else
        {
            RunPrompt();
        }
    }

    private static void RunPrompt()
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

    private static async Task RunFileAsync(string filePath)
    {
        var content = await File.ReadAllTextAsync(filePath);
        Run(content);
        if (HadError) Environment.Exit((int)SysExits.DataError);
    }

    private static void Run(string source)
    {
        throw new NotImplementedException();
        // Scanner scanner = new Scanner(source);
        // List<Token> tokens = scanner.ScanTokens();
        //
        // foreach(var token in tokens)
        // {
        //     Console.WriteLine(token);
        // }
    }

    private static void Error(int line, string message)
    {
        Report(line, "", message);
    }

    private static void Report(int line, string where, string message)
    {
        Console.WriteLine($"[{line}] Error{where}: {message}");
    }
}
