using AdventOfCode.Tools;

namespace Day09;

public class Program
{
    private const int _playerCount = 441;
    private const int _lastMarbleValue = 71032;
    private static void Main()
    {
        Console.WriteLine($"Part 1: {PlayMarbleGame(_playerCount, _lastMarbleValue)}");
        Console.WriteLine($"Part 2: {PlayMarbleGame(_playerCount, _lastMarbleValue * 100)}");
    }

    private static long PlayMarbleGame(int playerCount, int lastMarbleValue)
    {
        var players = new long[playerCount];
        var playerIndex = 0;
        var marbles = new LinkedList<int>();
        marbles.AddFirst(0);
        LinkedListNode<int> currentMarble = marbles.First!;

        for (int m = 1; m <= lastMarbleValue; m++)
        {
            if (m % 23 == 0)
            {
                currentMarble = currentMarble.GoBackNodes(7)!;
                players[playerIndex] += m + currentMarble!.Next!.Value;
                marbles.Remove(currentMarble.Next);
            }
            else
            {
                currentMarble = currentMarble.GoForwardNodes(2)!;
                marbles.AddAfter(currentMarble, m);
            }

            playerIndex = (playerIndex + 1) % playerCount;
        }

        return players.Max();
    }
}
