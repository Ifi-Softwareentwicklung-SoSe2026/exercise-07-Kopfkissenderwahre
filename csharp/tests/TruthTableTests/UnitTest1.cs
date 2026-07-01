using Input;
using LogicExpressions;
using TruthTable;
using System.IO;

namespace TruthTableTests;

public class UnitTest1
{
    private readonly TruthTermInputModule _inputModule = new();

    [Theory]
    [InlineData("A AND B", true, true, false, true)]
    [InlineData("A AND B", true, false, false, false)]
    [InlineData("A OR B", false, false, true, false)]
    [InlineData("A OR B", false, true, false, true)]
    [InlineData("NOT A", true, false, false, false)]
    [InlineData("NOT A", false, true, false, true)]
    public void Logical_operators_are_evaluated_correctly(
        string expression,
        bool a,
        bool b,
        bool c,
        bool expected)
    {
        TruthTerm truthTerm = _inputModule.Parse(expression);

        bool actual = TruthExpressionEvaluator.Evaluate(
            truthTerm.RootClause,
            new Dictionary<string, bool>
            {
                ["A"] = a,
                ["B"] = b,
                ["C"] = c
            });

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Parentheses_override_default_precedence()
    {
        TruthTerm truthTerm = _inputModule.Parse("(A OR B) AND C");

        bool actual = TruthExpressionEvaluator.Evaluate(
            truthTerm.RootClause,
            new Dictionary<string, bool>
            {
                ["A"] = true,
                ["B"] = false,
                ["C"] = false
            });

        Assert.False(actual);
    }

    [Theory]
    [InlineData("A", 2)]
    [InlineData("A AND B", 4)]
    [InlineData("A OR B AND C", 8)]
    public void Matrix_contains_2_pow_n_rows(string expression, int expectedRowCount)
    {
        TruthTerm truthTerm = _inputModule.Parse(expression);

        TruthTableMatrix matrix = TruthTableMatrixGenerator.Generate(truthTerm);

        Assert.Equal(expectedRowCount, matrix.Rows.Count);
        Assert.Equal(expectedRowCount, 1 << matrix.Variables.Count);
    }

    [Fact]
    public void Commandline_workflow_parses_and_writes_truth_table()
    {
        TextReader originalIn = Console.In;
        TextWriter originalOut = Console.Out;

        using var input = new StringReader("A AND B\n");
        using var output = new StringWriter();

        try
        {
            Console.SetIn(input);
            Console.SetOut(output);

            TruthTableGenerator.Run();
        }
        finally
        {
            Console.SetIn(originalIn);
            Console.SetOut(originalOut);
        }

        string[] lines = output.ToString()
            .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        Assert.Contains("Enter a logical expression (e.g. A AND (B OR NOT C)):", lines);
        Assert.Contains("Truth table:", lines);
        Assert.Contains("A | B | Result", lines);
        Assert.Contains("0 | 0 | 0", lines);
        Assert.Contains("0 | 1 | 0", lines);
        Assert.Contains("1 | 0 | 0", lines);
        Assert.Contains("1 | 1 | 1", lines);
    }

    [Fact]
    public void Empty_input_is_rejected()
    {
        ArgumentException exception = Assert.Throws<ArgumentException>(() => _inputModule.Parse(string.Empty));

        Assert.Equal("Unexpected end of expression.", exception.Message);
    }

    [Fact]
    public void Missing_closing_parenthesis_is_rejected()
    {
        ArgumentException exception = Assert.Throws<ArgumentException>(() => _inputModule.Parse("(A AND B"));

        Assert.Equal("Missing closing parenthesis.", exception.Message);
    }

    [Fact]
    public void Invalid_characters_are_rejected()
    {
        ArgumentException exception = Assert.Throws<ArgumentException>(() => _inputModule.Parse("A & B"));

        Assert.Equal("Invalid character '&'.", exception.Message);
    }

    [Theory]
    [InlineData("A XOR B")]
    [InlineData("A NAND B")]
    [InlineData("A NOR B")]
    public void Unsupported_operators_throw_NotImplementedException(string expression)
    {
        NotImplementedException exception = Assert.Throws<NotImplementedException>(() => _inputModule.Parse(expression));

        Assert.Contains("not implemented", exception.Message, StringComparison.OrdinalIgnoreCase);
    }
}
