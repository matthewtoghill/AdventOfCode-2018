namespace Day03;

public class Program
{
    private static readonly string[] input = Input.ReadAllLines();
    private static string[,] grid = new string[1000, 1000];
    private static int overlapping = 0;
    private static Dictionary<int, int> rectangles = new();

    private static void Main()
    {
        Solve();
        Console.WriteLine($"Part 1: {overlapping}");
        Console.WriteLine($"Part 2: {rectangles.First(x => x.Value == 0).Key}");
    }

    private static void Solve()
    {
        foreach (var line in input)
        {
            var split = line.SplitAs<int>(" ", "#", "@", "x", ":", ",").ToArray();
            rectangles.Add(split[0], 0);
            UpdateGrid(split[0], split[1], split[2], split[3], split[4]);
        }
    }

    private static void UpdateGrid(int id, int left, int top, int width, int height)
    {
        for (int x = top; x < top + height; x++)
        {
            for (int y = left; y < left + width; y++)
            {
                if (grid[x, y] is null)
                {
                    grid[x, y] = id.ToString();
                    continue;
                }

                if (grid[x, y] == "X")
                {
                    rectangles[id]++;
                    continue;
                }

                rectangles[int.Parse(grid[x, y])]++;
                rectangles[id]++;

                grid[x, y] = "X";
                overlapping++;
            }
        }
    }
}
