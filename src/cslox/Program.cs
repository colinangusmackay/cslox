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
            await Lox.RunFileAsync(args[0]);
        }
        else
        {
            Lox.RunPrompt();
        }
    }
}
