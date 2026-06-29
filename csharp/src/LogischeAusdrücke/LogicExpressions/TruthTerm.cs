namespace LogicExpressions;

/// <summary>
/// Represents a parsed logical term with original text and expression tree.
/// </summary>
public sealed class TruthTerm
{
    /// <summary>
    /// Initializes a truth term.
    /// </summary>
    /// <param name="rawInput">Original user input.</param>
    /// <param name="rootClause">Root node of the parsed expression tree.</param>
    public TruthTerm(string rawInput, IExpressionNode rootClause)
    {
        RawInput = rawInput;
        RootClause = rootClause;
    }

    /// <summary>
    /// Gets the original input string.
    /// </summary>
    public string RawInput { get; }

    /// <summary>
    /// Gets the root node of the expression tree.
    /// </summary>
    public IExpressionNode RootClause { get; }
}
