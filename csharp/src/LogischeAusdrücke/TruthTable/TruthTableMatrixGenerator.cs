using System;
using System.Collections.Generic;
using LogicExpressions;
using ParsedTruthTerm = LogicExpressions.TruthTerm;

namespace TruthTable;

/// <summary>
/// Builds truth-table matrix data for parsed terms.
/// </summary>
public static class TruthTableMatrixGenerator
{
    /// <summary>
    /// Generates a full truth-table matrix for a parsed term.
    /// </summary>
    /// <param name="truthTerm">Parsed logical term.</param>
    /// <returns>Generated truth-table matrix.</returns>
    public static TruthTableMatrix Generate(ParsedTruthTerm truthTerm)
    {
        List<string> variables = TruthVariableCollector.ExtractVariables(truthTerm.RootClause);
        int variableCount = variables.Count;
        int rowCount = 1 << variableCount;
        var rows = new List<TruthTableRow>(rowCount);

        for (int i = 0; i < rowCount; i++)
        {
            Dictionary<string, bool> assignments = CreateAssignments(variables, i);
            bool result = TruthExpressionEvaluator.Evaluate(truthTerm.RootClause, assignments);
            rows.Add(new TruthTableRow(assignments, result));
        }

        return new TruthTableMatrix(variables, rows);
    }

    /// <summary>
    /// Creates one variable assignment map for a row index.
    /// </summary>
    /// <param name="variables">Ordered variable names.</param>
    /// <param name="rowIndex">Row index encoded as bit pattern.</param>
    /// <returns>Variable assignment map.</returns>
    private static Dictionary<string, bool> CreateAssignments(IReadOnlyList<string> variables, int rowIndex)
    {
        var assignments = new Dictionary<string, bool>(variables.Count, StringComparer.Ordinal);

        for (int i = 0; i < variables.Count; i++)
        {
            int bitIndex = variables.Count - i - 1;
            bool value = ((rowIndex >> bitIndex) & 1) == 1;
            assignments[variables[i]] = value;
        }

        return assignments;
    }
}
