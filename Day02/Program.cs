namespace Day02;

public class Program
{
    private static readonly string[] input  = File.ReadAllLines(@"..\..\..\..\data\day02.txt");
    private static void Main()
    {
        Console.WriteLine($"Part 1: {Part1()}");
        Console.WriteLine($"Part 2: {Part2()}");
    }

    private static int Part1()
    {
        int doubleTotal = 0, tripleTotal = 0;

        foreach (var line in input)
        {
            var duplicateCounts = GetCharFrequencies(line).Values;
            if (duplicateCounts.Any(x => x == 2)) doubleTotal++;
            if (duplicateCounts.Any(x => x == 3)) tripleTotal++;
        }

        return doubleTotal * tripleTotal;
    }

    private static string Part2()
    {
        for (int i = 0; i < input.Length; i++)
        {
            var line = input[i];

            for (int j = i + 1; j < input.Length; j++)
            {
                var otherLine = input[j];
                var diffCount = GetDiffCount(line, otherLine);

                if (diffCount == 1)
                    return RemoveDiffChars(line, otherLine);
            }
        }
        return "";
    }

    private static Dictionary<char, int> GetCharFrequencies(string text) => text.GroupBy(c => c).ToDictionary(x => x.Key, x => x.Count());
    private static int GetDiffCount(string a, string b) => a.Zip(b, (c1, c2) => c1 == c2).Count(x => !x);
    private static string RemoveDiffChars(string a, string b) => string.Join("", a.Zip(b, (c1, c2) => c1 == c2 ? c1.ToString() : ""));
}
