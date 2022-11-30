using AdventOfCode.Tools;

namespace Day04;

public class Program
{
    private static readonly string[] _input = File.ReadAllLines(@"..\..\..\..\data\day04.txt");
    private static readonly List<GuardDuty> _guardDuties = ParseGuardActivityLog(_input);
    private static void Main()
    {
        Console.WriteLine($"Part 1: {Part1()}");
        Console.WriteLine($"Part 2: {Part2()}");
    }

    private static List<GuardDuty> ParseGuardActivityLog(string[] input)
    {
        var activityList = input.Select(x => new GuardActivity(x)).OrderBy(x => x.ActivityDateTime).ToList();
        List<GuardDuty> guardDuties = new();
        GuardDuty currentDay = null!;

        for (int i = 0; i < activityList.Count; i++)
        {
            var item = activityList[i];
            switch (item.ActivityType)
            {
                case ActivityType.NewGuardOnDuty:
                    currentDay = new GuardDuty(item.Id);
                    guardDuties.Add(currentDay);
                    break;
                case ActivityType.FallsAsleep:
                    currentDay!.RecordTimeAsleep(item.ActivityDateTime.Minute, activityList[i + 1].ActivityDateTime.Minute);
                    break;
                default:
                    break;
            }
        }

        return guardDuties;
    }

    private static int Part1()
    {
        var sleepiestGuard = _guardDuties.GroupBy(x => x.Id)
                                         .Select(g => new { Id = g.Key, Total = g.SelectMany(x => x.AsleepMinutes).Count() })
                                         .MaxBy(x => x.Total)!.Id;

        var maxMinuteAsleep = _guardDuties.Where(x => x.Id == sleepiestGuard)
                                          .SelectMany(x => x.AsleepMinutes)
                                          .GroupBy(x => x)
                                          .ToDictionary(x => x.Key, x => x.Count())
                                          .MaxBy(x => x.Value).Key;

        return sleepiestGuard * maxMinuteAsleep;
    }

    private static int Part2()
    {
        Dictionary<(int, int), int> guardAsleepMinuteFrequency = new();

        foreach (var item in _guardDuties)
            foreach (var min in item.AsleepMinutes)
                guardAsleepMinuteFrequency.IncrementAt((item.Id, min));

        var (id, minute) = guardAsleepMinuteFrequency.MaxBy(x => x.Value).Key;
        return id * minute;
    }
}
