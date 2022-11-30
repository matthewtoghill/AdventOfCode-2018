using AdventOfCode.Tools;

namespace Day07;

public class Program
{
    private static readonly string[] _input = File.ReadAllLines(@"..\..\..\..\data\day07.txt");
    private static readonly Dictionary<string, string> _baseSteps = GenerateSteps(_input);
    private static void Main()
    {
        Console.WriteLine($"Part 1: {Part1()}");
        Console.WriteLine($"Part 2: {Part2(5)}");
    }

    private static string Part1()
    {
        var steps = new Dictionary<string, string>(_baseSteps);
        var completedTasks = "";

        while (steps.Count > 0)
        {
            completedTasks += CompleteNextTask(steps);
        }

        return completedTasks;
    }

    private static int Part2(int elfCount)
    {
        var steps = new Dictionary<string, string>(_baseSteps);
        var elves = Enumerable.Range(0, elfCount).Select(_ => new Elf()).ToList();
        var totalTime = 0;

        while (steps.Count > 0)
        {
            foreach (var elf in elves)
            {
                if (!elf.HasTask)
                {
                    var nextTask = GetNextTask(steps);
                    if (nextTask != '|' && elf.TryAssignNewTask(nextTask))
                        steps[nextTask.ToString()] = "|";
                }
                else
                {
                    elf.Tick();
                }
            }

            foreach (var elf in elves.Where(e => !e.HasTask))
                ClearCompletedTask(steps, elf.CurrentTask.ToString());

            totalTime++;
        }

        return totalTime;
    }

    private static char GetNextTask(Dictionary<string, string> tasks)
    {
        char nextTask = '|';

        foreach (var (key, value) in tasks)
        {
            if (value.Length == 0)
            {
                if ((nextTask != '|' && key[0] < nextTask) || nextTask == '|')
                    nextTask = key[0];
            }
        }

        return nextTask;
    }

    private static string CompleteNextTask(Dictionary<string, string> tasks)
    {
        string nextTask = "";

        foreach (var (key, value) in tasks)
        {
            if (value.Length == 0)
            {
                if ((!string.IsNullOrWhiteSpace(nextTask) && key[0] < nextTask[0]) || string.IsNullOrWhiteSpace(nextTask))
                    nextTask = key;
            }
        }

        if (!string.IsNullOrWhiteSpace(nextTask))
            ClearCompletedTask(tasks, nextTask);

        return nextTask;
    }

    private static void ClearCompletedTask(Dictionary<string, string> tasks, string completedTask)
    {
        tasks.Remove(completedTask);
        tasks.Keys.ToList().ForEach(key => tasks[key] = tasks[key].Replace(completedTask, ""));
    }

    private static Dictionary<string, string> GenerateSteps(string[] input)
    {
        var steps = new Dictionary<string, string>();
        foreach (var line in input)
        {
            var split = line.Split();
            var preRequirement = split[1];
            var step = split[7];
            steps.AppendAt(step, preRequirement);

            if (!steps.ContainsKey(preRequirement))
                steps.Add(preRequirement, string.Empty);
        }

        return steps;
    }
}
