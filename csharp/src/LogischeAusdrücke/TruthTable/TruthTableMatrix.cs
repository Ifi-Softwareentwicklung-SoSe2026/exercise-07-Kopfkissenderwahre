using System;
using System.Collections.Generic;

namespace TruthTable;

/// <summary>
/// Represents a complete truth-table matrix.
/// </summary>
public sealed class TruthTableMatrix
{
    /// <summary>
    /// Initializes a truth-table matrix.
    /// </summary>
    /// <param name="variables">Ordered list of variable names.</param>
    /// <param name="rows">Computed truth-table rows.</param>
    public TruthTableMatrix(IReadOnlyList<string> variables, IReadOnlyList<TruthTableRow> rows)
    {
        Variables = variables;
        Rows = rows;
    }

    /// <summary>
    /// Gets variable names in column order.
    /// </summary>
    public IReadOnlyList<string> Variables { get; }

    /// <summary>
    /// Gets all rows of the truth table.
    /// </summary>
    public IReadOnlyList<TruthTableRow> Rows { get; }
}

/// <summary>
/// Represents one row in a truth table.
/// </summary>
public sealed class TruthTableRow
{
    /// <summary>
    /// Initializes a truth-table row.
    /// </summary>
    /// <param name="assignments">Variable values for this row.</param>
    /// <param name="result">Evaluation result for this row.</param>
    public TruthTableRow(IReadOnlyDictionary<string, bool> assignments, bool result)
    {
        Assignments = assignments;
        Result = result;
    }

    /// <summary>
    /// Gets variable assignments for this row.
    /// </summary>
    public IReadOnlyDictionary<string, bool> Assignments { get; }

    /// <summary>
    /// Gets the calculated result for this row.
    /// </summary>
    public bool Result { get; }

    /// <summary>
    /// Returns the boolean value for a variable in this row.
    /// </summary>
    /// <param name="variableName">Variable name.</param>
    /// <returns>Assigned boolean value.</returns>
    public bool GetValue(string variableName)
    {
        if (!Assignments.TryGetValue(variableName, out bool value))
        {
            throw new ArgumentException($"Unknown variable '{variableName}'.", nameof(variableName));
        }

        return value;
    }

    /// <summary>
    /// Returns the display value for a variable as 0 or 1.
    /// </summary>
    /// <param name="variableName">Variable name.</param>
    /// <returns>Display value.</returns>
    public string GetDisplayValue(string variableName)
    {
        return GetValue(variableName) ? "1" : "0";
    }

    /// <summary>
    /// Returns the result display value as 0 or 1.
    /// </summary>
    /// <returns>Display value.</returns>
    public string GetResultDisplayValue()
    {
        return Result ? "1" : "0";
    }
}
