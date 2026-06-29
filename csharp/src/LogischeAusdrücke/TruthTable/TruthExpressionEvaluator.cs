using System;
using System.Collections.Generic;
using LogicExpressions;

namespace TruthTable;

/// <summary>
/// Evaluates expression tree nodes against a variable assignment.
/// </summary>
public static class TruthExpressionEvaluator
{
    /// <summary>
    /// Evaluates one expression node.
    /// </summary>
    /// <param name="node">Expression node to evaluate.</param>
    /// <param name="assignments">Variable assignment map.</param>
    /// <returns>Computed boolean value.</returns>
    public static bool Evaluate(IExpressionNode node, IReadOnlyDictionary<string, bool> assignments)
    {
        return node switch
        {
            Variable variable => EvaluateVariable(variable, assignments),
            LogicClause logicClause => EvaluateClause(logicClause, assignments),
            _ => throw new NotSupportedException($"Unsupported node type '{node.GetType().Name}'.")
        };
    }

    /// <summary>
    /// Evaluates a variable node by looking up its assignment.
    /// </summary>
    /// <param name="variable">Variable node.</param>
    /// <param name="assignments">Variable assignment map.</param>
    /// <returns>Assigned boolean value.</returns>
    private static bool EvaluateVariable(Variable variable, IReadOnlyDictionary<string, bool> assignments)
    {
        if (!assignments.TryGetValue(variable.Name, out bool value))
        {
            throw new ArgumentException($"Missing assignment for variable '{variable.Name}'.", nameof(assignments));
        }

        return value;
    }

    /// <summary>
    /// Evaluates a clause node based on its operator.
    /// </summary>
    /// <param name="logicClause">Clause node.</param>
    /// <param name="assignments">Variable assignment map.</param>
    /// <returns>Computed boolean value.</returns>
    private static bool EvaluateClause(LogicClause logicClause, IReadOnlyDictionary<string, bool> assignments)
    {
        return logicClause.Operation switch
        {
            LogicOperator.And => Evaluate(logicClause.LeftOperand, assignments)
                && Evaluate(logicClause.RightOperand!, assignments),
            LogicOperator.Or => Evaluate(logicClause.LeftOperand, assignments)
                || Evaluate(logicClause.RightOperand!, assignments),
            LogicOperator.Not => !Evaluate(logicClause.LeftOperand, assignments),
            LogicOperator.Xor => throw new NotImplementedException("XOR is not implemented."),
            LogicOperator.Nand => throw new NotImplementedException("NAND is not implemented."),
            LogicOperator.Nor => throw new NotImplementedException("NOR is not implemented."),
            _ => throw new NotSupportedException($"Unsupported operator '{logicClause.Operation}'.")
        };
    }
}
