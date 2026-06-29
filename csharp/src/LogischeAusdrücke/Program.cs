using System;
using TruthTable;
using TruthTermSimplifier;

/// <summary>
/// Application entry point and command dispatcher.
/// </summary>
public static class Program
{
    private const string TABLE = "tabele";
    private const string SIMPLIFY = "vereinfachen";

    /// <summary>
    /// Executes the selected command based on command line arguments.
    /// </summary>
    /// <param name="args">Command line arguments.</param>
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            PrintHelp();
            return;
        }

        string command = args[0].ToLowerInvariant();

        switch (command)
        {
            case TABLE:
                TruthTableGenerator.Run();
                break;

            case SIMPLIFY:
                TruthTermSimplifier.TruthTermSimplifier.Run();
                break;

            default:
                PrintHelp();
                break;
        }
    }

    /// <summary>
    /// Prints command usage information.
    /// </summary>
    private static void PrintHelp()
    {
        Console.WriteLine("Usage:");
        Console.WriteLine("  dotnet run tabelle");
        Console.WriteLine("  dotnet run vereinfachen");
    }
}