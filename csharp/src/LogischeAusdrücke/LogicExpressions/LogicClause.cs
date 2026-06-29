using System;

namespace LogicExpressions;

/// <summary>
/// Represents a logical clause node with unary or binary operands.
/// </summary>
public sealed class LogicClause : IExpressionNode
{
    /// <summary>
    /// Initializes a logical clause with the provided operator and operands.
    /// </summary>
    /// <param name="operation">Logical operator for this clause.</param>
    /// <param name="leftOperand">Left operand or single operand for NOT.</param>
    /// <param name="rightOperand">Right operand for binary operators.</param>
    public LogicClause(LogicOperator operation, IExpressionNode leftOperand, IExpressionNode? rightOperand = null)
    {
        if (operation == LogicOperator.Not && rightOperand is not null)
        {
            throw new ArgumentException("NOT accepts exactly one operand.", nameof(rightOperand));
        }

        if (operation != LogicOperator.Not && rightOperand is null)
        {
            throw new ArgumentException("Binary operators require two operands.", nameof(rightOperand));
        }

        Operation = operation;
        LeftOperand = leftOperand;
        RightOperand = rightOperand;
    }

    /// <summary>
    /// Gets the operator associated with this clause.
    /// </summary>
    public LogicOperator Operation { get; }

    /// <summary>
    /// Gets the left operand.
    /// </summary>
    public IExpressionNode LeftOperand { get; }

    /// <summary>
    /// Gets the right operand for binary operators.
    /// </summary>
    public IExpressionNode? RightOperand { get; }
}
