namespace cslox;

public class Program
{
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
        Console.WriteLine("cslox REPL. Press Ctrl+C to exit.");
        while (true)
        {
            Console.Write("> ");
            var line = Console.ReadLine();
            if (line == null)
            {
                Console.WriteLine();
                continue;
            }
            Run(line);
        }
    }

    private static async Task RunFileAsync(string filePath)
    {
        var content = await File.ReadAllTextAsync(filePath);
        Run(content);
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
}
