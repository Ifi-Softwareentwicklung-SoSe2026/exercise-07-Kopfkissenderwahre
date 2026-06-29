using System;
using System.Collections.Generic;
using System.Linq;
using LogicExpressions;

namespace TruthTable;

/// <summary>
/// Collects variable identifiers from expression trees.
/// </summary>
public static class TruthVariableCollector
{
    /// <summary>
    /// Extracts distinct variable names from a root expression node.
    /// </summary>
    /// <param name="rootClause">Expression root node.</param>
    /// <returns>Sorted list of variable names.</returns>
    public static List<string> ExtractVariables(IExpressionNode rootClause)
    {
        var variables = new HashSet<string>(StringComparer.Ordinal);
        CollectVariables(rootClause, variables);

        return [.. variables.OrderBy(name => name, StringComparer.Ordinal)];
    }

    /// <summary>
    /// Traverses the expression tree and adds variable names to a set.
    /// </summary>
    /// <param name="node">Current expression node.</param>
    /// <param name="variables">Target variable set.</param>
    private static void CollectVariables(IExpressionNode node, HashSet<string> variables)
    {
        switch (node)
        {
            case Variable variable:
                variables.Add(variable.Name);
                break;

            case LogicClause logicClause:
                CollectVariables(logicClause.LeftOperand, variables);

                if (logicClause.RightOperand is not null)
                {
                    CollectVariables(logicClause.RightOperand, variables);
                }

                break;

            default:
                throw new NotSupportedException($"Unsupported node type '{node.GetType().Name}'.");
        }
    }
}
