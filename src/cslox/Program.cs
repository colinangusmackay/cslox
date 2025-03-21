namespace cslox;

public class Program
{
    public static async Task Main(string[] args)
    {
        double one = 1.0;
        double half = 0.5;

        Console.WriteLine(one.ToString());
        Console.WriteLine(half.ToString());

        return;

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
