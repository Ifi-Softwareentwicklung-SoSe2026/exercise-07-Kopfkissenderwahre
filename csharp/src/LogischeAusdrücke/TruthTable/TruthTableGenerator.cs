using System;
using Input;
using ParsedTruthTerm = LogicExpressions.TruthTerm;

namespace TruthTable;

/// <summary>
/// Coordinates input parsing and truth-table generation workflow.
/// </summary>
public static class TruthTableGenerator
{
    /// <summary>
    /// Reads an expression from console input and runs table generation.
    /// </summary>
    public static void Run()
    {
        Console.WriteLine("Enter a logical expression (e.g. A AND (B OR NOT C)):");
        var inputModule = new TruthTermInputModule();

        try
        {
            ParsedTruthTerm truthTerm = inputModule.ReadFromConsole();
            GenerateTruthTable(truthTerm);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Input error: {ex.Message}");
        }
    }

    /// <summary>
    /// Generates and writes a truth table for the provided term.
    /// </summary>
    /// <param name="truthTerm">Parsed logical term.</param>
    public static void GenerateTruthTable(ParsedTruthTerm truthTerm)
    {
        TruthTableMatrix matrix = TruthTableMatrixGenerator.Generate(truthTerm);
        TruthTableWriter.Write(matrix);
    }
}
