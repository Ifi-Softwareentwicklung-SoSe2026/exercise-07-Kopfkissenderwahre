namespace LogicExpressions;

/// <summary>
/// Represents a variable node in the expression tree.
/// </summary>
public sealed class Variable : IExpressionNode
{
    /// <summary>
    /// Initializes a variable node.
    /// </summary>
    /// <param name="name">Normalized variable name.</param>
    public Variable(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Gets the variable name.
    /// </summary>
    public string Name { get; }
}
