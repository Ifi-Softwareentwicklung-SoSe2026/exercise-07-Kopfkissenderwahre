using System;
using Input;
using LogicExpressions;
using ParsedTruthTerm = LogicExpressions.TruthTerm;

namespace TruthTermSimplifier;

/// <summary>
/// Entry point for truth-term simplification workflows.
/// </summary>
public static class TruthTermSimplifier
{
    /// <summary>
    /// Reads an expression from the console and runs simplification.
    /// </summary>
    public static void Run()
    {
        Console.WriteLine("Enter a logical expression to simplify:");
        var inputModule = new TruthTermInputModule();

        try
        {
            ParsedTruthTerm truthTerm = inputModule.ReadFromConsole();
            Simplify(truthTerm);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Input error: {ex.Message}");
        }
    }

    /// <summary>
    /// Simplifies a parsed truth term.
    /// </summary>
    /// <param name="truthTerm">Parsed logical term.</param>
    public static void Simplify(ParsedTruthTerm truthTerm)
    {
        Console.WriteLine("Truth term simplification is not implemented yet.");
        Console.WriteLine($"Input term: {truthTerm.RawInput}");
    }
}
