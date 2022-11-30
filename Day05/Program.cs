using System.Text;

namespace Day05;

public class Program
{
    private static readonly string _input = File.ReadAllText(@"..\..\..\..\data\day05.txt");
    private static void Main()
    {
        Console.WriteLine($"Part 1: {ReactPolymer(_input)}");
        Console.WriteLine($"Part 2: {Part2()}");
    }

    private static int Part2()
    {
        var results = Enumerable.Range(0, 26)
                                .Select(x => (char)('a' + x))
                                .Select(x => ReactPolymer(_input.Replace(x.ToString(), "").Replace(x.ToString().ToUpper(), "")));

        return results.Min();
    }

    private static int ReactPolymer(string polymer)
    {
        var reactedPolymer = new StringBuilder(polymer);
        bool hasAnyReaction;
        do
        {
            hasAnyReaction = false;
            for (int i = 0; i < reactedPolymer.Length - 1; i++)
            {
                if (HasReaction(reactedPolymer[i], reactedPolymer[i + 1]))
                {
                    hasAnyReaction = true;
                    reactedPolymer.Remove(i, 2);
                    i -= 2;
                    if (i < 0) i = 0;
                }
            }
        } while (hasAnyReaction);

        return reactedPolymer.Length;
    }

    // lowercase and uppercase characters are 32 apart, use xor (^) to confirm a reaction
    private static bool HasReaction(char a, char b) => (a ^ b) == 32;
}
