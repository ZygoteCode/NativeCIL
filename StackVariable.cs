public class StackVariable
{
    public int index;

    public StackVariable(int index)
    {
        this.index = index;
    }

    public override string ToString()
    {
        return "variable" + this.index.ToString();
    }
}