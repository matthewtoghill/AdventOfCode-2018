namespace Day06;

public class Program
{
    private const int MAX_TOTAL_DISTANCE = 10_000;
    private static readonly Coordinate[] _points = Input.ReadAllLines().Select(x => x.Split(", "))
                                                        .Select(x => new Coordinate(x[0], x[1])).ToArray();

    private static void Main()
    {
        var (largest, safeArea) = Solve();
        Console.WriteLine($"Part 1: {largest}");
        Console.WriteLine($"Part 2: {safeArea}");
    }

    private static (int largest, int safeArea) Solve()
    {
        int xMax = _points.Max(x => x.X) + 1;
        int yMax = _points.Max(x => x.Y) + 1;

        var grid = new int[xMax, yMax];
        var safeArea = 0;

        for (int x = 0; x < xMax; x++)
        {
            for (int y = 0; y < yMax; y++)
            {
                var distances = _points.Select(p => (p.Id, distance: p.ManhattanDistance(x, y)))
                                       .OrderBy(x => x.distance).ToArray();
                grid[x, y] = distances[0].distance != distances[1].distance ? distances[0].Id : -1;

                if (distances.Sum(x => x.distance) < MAX_TOTAL_DISTANCE) safeArea++;
            }
        }

        var excluded = new HashSet<int>();
        var counts = new Dictionary<int, int>();

        for (int x = 0; x < xMax; x++)
        {
            for (int y = 0; y < yMax; y++)
            {
                if (x == 0 || y == 0 || x == xMax - 1 || y == yMax - 1)
                    excluded.Add(grid[x, y]);

                counts.IncrementAt(grid[x, y]);
            }
        }

        var largest = counts.Where(k => !excluded.Contains(k.Key)).Max(k => k.Value);

        return (largest, safeArea);
    }
}
