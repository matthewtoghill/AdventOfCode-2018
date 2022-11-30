namespace Day03;

public class Program
{
    private static readonly string[] input = File.ReadAllLines(@"..\..\..\..\data\day03.txt");
    private static string[,] grid = new string[1000, 1000];
    private static int overlapping = 0;
    private static Dictionary<int, int> rectangles = new();

    private static void Main()
    {
        Solve();
        Console.WriteLine($"Part 1: {overlapping}");
        Console.WriteLine($"Part 2: {rectangles.Where(x => x.Value == 0).First().Key}");
    }

    private static void Solve()
    {
        foreach (var line in input)
        {
            var split = line.Split(new[] { ' ', '#', '@', 'x', ':', ',' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
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
