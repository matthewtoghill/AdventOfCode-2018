using System.Text;

namespace Day12;

public class Program
{
    private static readonly string input = Input.ReadAll();
    private static void Main()
    {
        var split = input.SplitOn("\n\n");
        var state = $".....{split[0][15..]}.....";
        var rules = split[1].Split('\n').Select(x => new Rule(x)).ToList();
        var maxNegativeIndex = -5;
        var scores = new List<long>();
        var stabilityCount = 0;
        long lastDiff = 0;

        for (long g = 0; g < 50_000_000_000; g++)
        {
            var nextState = new StringBuilder(state);
            for (int i = 0; i < state.Length - 5; i++)
            {
                var pots = state[i..(i + 5)];

                foreach (var rule in rules)
                {
                    if (pots == rule.Pattern)
                    {
                        nextState[i + 2] = rule.GrowsPlant ? '#' : '.';
                        break;
                    }
                }
            }

            state = nextState.ToString();
            if (!state.StartsWith("...."))
            {
                state = "...." + state;
                maxNegativeIndex -= 4;
            }

            if (!state.EndsWith("....")) state += "....";

            scores.Add(ScorePots(state, maxNegativeIndex));
            if (scores.Count <= 1) continue;

            // check if score differences have stabilised
            var diff = scores[^1] - scores[^2];
            if (diff == lastDiff)
            {
                stabilityCount++;
            }
            else
            {
                stabilityCount = 0;
                lastDiff = diff;
            }

            if (stabilityCount > 3)
                break;
        }

        Console.WriteLine($"Part 1: {scores[19]}");
        Console.WriteLine($"Part 2: {scores[^1] + ((scores[^1] - scores[^2]) * (50_000_000_000 - scores.Count))}");
    }

    private static long ScorePots(string pots, int startIndex)
    {
        long score = 0;
        for (int i = 0; i < pots.Length; i++)
        {
            if (pots[i] == '#') score += startIndex;
            startIndex++;
        }
        return score;
    }
}
