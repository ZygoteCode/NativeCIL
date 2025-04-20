public class StackExpression
{
    public string expression;

    public StackExpression(string expression)
    {
        this.expression = expression;
    }

    public override string ToString()
    {
        return this.expression;
    }
}