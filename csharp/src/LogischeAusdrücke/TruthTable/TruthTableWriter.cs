using System;

namespace TruthTable;

/// <summary>
/// Writes truth-table matrix data to the console.
/// </summary>
public static class TruthTableWriter
{
    /// <summary>
    /// Writes the matrix in tabular form.
    /// </summary>
    /// <param name="matrix">Matrix to write.</param>
    public static void Write(TruthTableMatrix matrix)
    {
        Console.WriteLine("Truth table:");
        foreach (string variable in matrix.Variables)
        {
            Console.Write($"{variable} | ");
        }

        Console.WriteLine("Result");

        foreach (TruthTableRow row in matrix.Rows)
        {
            foreach (string variableName in matrix.Variables)
            {
                Console.Write($"{row.GetDisplayValue(variableName)} | ");
            }

            Console.WriteLine(row.GetResultDisplayValue());
        }
    }
}
