namespace AdventOfCode.Tools;

public static class LinkedListExtensions
{
    public static LinkedListNode<T> NextNode<T>(this LinkedListNode<T> node) => node.Next ?? node.List!.First!;
    public static LinkedListNode<T> PrevNode<T>(this LinkedListNode<T> node) => node.Previous ?? node.List!.Last!;

    public static LinkedListNode<T>? GoForwardNodes<T>(this LinkedListNode<T> node, int steps)
    {
        if (steps == 0) return node;
        if (steps < 0) return node.GoBackNodes(-steps);

        for (int i = 0; i < steps; i++)
            node = node.NextNode()!;

        return node;
    }

    public static LinkedListNode<T>? GoBackNodes<T>(this LinkedListNode<T> node, int steps)
    {
        if (steps == 0) return node;
        if (steps < 0) return node.GoForwardNodes(-steps);

        for (int i = 0; i < steps; i++)
            node = node.PrevNode()!;

        return node;
    }
}
