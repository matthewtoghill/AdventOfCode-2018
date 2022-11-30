namespace Day11;

public class Program
{
    private const int _serialNumber = 7689;
    private static void Main()
    {
        var grid = CreatePowerGrid();

        Console.WriteLine($"Part 1: {Part1(grid)}");
        Console.WriteLine($"Part 2: {Part2(grid)}");
    }

    private static string Part1(int[,] grid)
    {
        var gridPowers = CalculateGridPower(grid, 3);
        var max = gridPowers.MaxBy(x => x.total);
        return $"{max.total} at {max.x},{max.y}";
    }

    private static string Part2(int[,] grid)
    {
        var maxGridPowers = new List<(int x, int y, int size, int total)>();
        for (int i = 1; i < 300; i++)
        {
            var maxP = CalculateGridPower(grid, i).MaxBy(p => p.total);
            if (maxP.total < 0)
                break;

            maxGridPowers.Add(maxP);
        }

        var max = maxGridPowers.MaxBy(x => x.total);
        return $"{max.total} at {max.x},{max.y},{max.size}";
    }

    private static List<(int x, int y, int size, int total)> CalculateGridPower(int[,] grid, int squareSize)
    {
        var squares = new List<(int, int, int, int)>();

        for (int x = 1; x < 301 - squareSize; x++)
            for (int y = 1; y < 301 - squareSize; y++)
                squares.Add((x, y, squareSize, Enumerable.Range(x, squareSize)
                                                         .SelectMany(x2 => Enumerable.Range(y, squareSize)
                                                         .Select(y2 => grid[x2, y2]))
                                                         .Sum()));

        return squares;
    }

    private static int[,] CreatePowerGrid()
    {
        var grid = new int[301, 301];

        for (int x = 1; x <= 300; x++)
            for (int y = 1; y <= 300; y++)
                grid[x, y] = GetPowerLevel(x, y);

        return grid;
    }

    private static int GetPowerLevel(int x, int y)
    {
        var rackId = x + 10;
        var powerLevel = ((rackId * y) + _serialNumber) * rackId;
        return (powerLevel % 1000 / 100) - 5;
    }
}
