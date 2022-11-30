namespace Day08;

public class Node
{
    public List<Node> ChildNodes = new();
    public List<int> MetaData = new();

    public int SumMeta() => MetaData.Sum() + ChildNodes.Sum(x => x.SumMeta());

    public int Value()
    {
        if (ChildNodes.Count == 0)
            return MetaData.Sum();

        int result = 0;

        foreach (var m in MetaData)
            if (m <= ChildNodes.Count)
                result += ChildNodes[m - 1].Value();

        return result;
    }
}
