namespace Day08;

public class Program
{
    private static readonly int[] _input = File.ReadAllText(@"..\..\..\..\data\day08.txt").Split().Select(int.Parse).ToArray();
    private static void Main()
    {
        int index = 0;
        Node start = ReadNode(_input, ref index);
        Console.WriteLine($"Part 1: {start.SumMeta()}");
        Console.WriteLine($"Part 2: {start.Value()}");
    }

    private static Node ReadNode(int[] numbers, ref int index)
    {
        var newNode = new Node();
        var children = numbers[index++];
        var metadata = numbers[index++];

        for (int i = 0; i < children; i++)
            newNode.ChildNodes.Add(ReadNode(numbers, ref index));

        for (int i = 0; i < metadata; i++)
            newNode.MetaData.Add(numbers[index++]);

        return newNode;
    }
}
