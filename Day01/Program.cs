namespace Day01;

public class Program
{
    private static readonly int[] frequencyChanges = Input.ReadAllLinesAs<int>().ToArray();
    private static void Main()
    {
        Console.WriteLine($"Part 1: {Part1()}");
        Console.WriteLine($"Part 2: {Part2()}");
    }

    private static int Part1() => frequencyChanges.Sum();

    private static string Part2()
    {
        var frequency = 0;
        var frequencyHistory = new Dictionary<int, int>() { [frequency] = 1 };

        while (true)
        {
            foreach (var val in frequencyChanges)
            {
                frequency += val;

                if (frequencyHistory.ContainsKey(frequency))
                    return $"{frequency} ({frequencyHistory.Count} frequency changes)";

                frequencyHistory[frequency] = 1;
            }
        }
    }
}
