namespace LogicExpressions;

/// <summary>
/// Supported logical operators in expression clauses.
/// </summary>
public enum LogicOperator
{
    /// <summary>Logical conjunction.</summary>
    And,
    /// <summary>Logical disjunction.</summary>
    Or,
    /// <summary>Logical negation.</summary>
    Not,
    /// <summary>Exclusive OR.</summary>
    Xor,
    /// <summary>Negated AND.</summary>
    Nand,
    /// <summary>Negated OR.</summary>
    Nor
}
