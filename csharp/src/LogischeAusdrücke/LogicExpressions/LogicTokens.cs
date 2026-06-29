using System;
using System.Collections.Generic;

namespace LogicExpressions;

/// <summary>
/// Provides token definitions and token classification helpers.
/// </summary>
public static class LogicTokens
{
    /// <summary>
    /// Gets all reserved operator keywords.
    /// </summary>
    public static readonly IReadOnlySet<string> ReservedKeywords = new HashSet<string>(StringComparer.Ordinal)
    {
        "AND",
        "OR",
        "NOT",
        "XOR",
        "NAND",
        "NOR"
    };

    /// <summary>
    /// Gets all punctuation tokens used by the parser.
    /// </summary>
    public static readonly IReadOnlySet<string> Punctuation = new HashSet<string>(StringComparer.Ordinal)
    {
        "(",
        ")"
    };

    /// <summary>
    /// Determines whether the token is a reserved operator keyword.
    /// </summary>
    /// <param name="token">Token to classify.</param>
    /// <returns><c>true</c> when the token is reserved.</returns>
    public static bool IsReserved(string token)
    {
        return ReservedKeywords.Contains(token);
    }

    /// <summary>
    /// Determines whether the token is parser punctuation.
    /// </summary>
    /// <param name="token">Token to classify.</param>
    /// <returns><c>true</c> when the token is punctuation.</returns>
    public static bool IsPunctuation(string token)
    {
        return Punctuation.Contains(token);
    }

    /// <summary>
    /// Determines whether the token is treated as an identifier.
    /// </summary>
    /// <param name="token">Token to classify.</param>
    /// <returns><c>true</c> when the token is an identifier.</returns>
    public static bool IsIdentifier(string token)
    {
        return !IsReserved(token) && !IsPunctuation(token);
    }
}
