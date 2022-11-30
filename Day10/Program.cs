using System.Text;
using AdventOfCode.Tools;

namespace Day10;

public class Program
{
    private static readonly List<Point> points = File.ReadAllLines(@"..\..\..\..\data\day10.txt").Select(CreatePoint).ToList();
    private static void Main()
    {
        var minP = points.MinBy(p => p.Velocity.X + p.Velocity.Y)!; // point with the minimum velocity (e.g. -5, -5)
        var maxP = points.MaxBy(p => p.Velocity.X + p.Velocity.Y)!; // point with the maximum velocity (e.g. 5, 5)
        var distance = minP.ManhattanDistance(maxP);
        var ticks = 0;

        while (true)
        {
            // Calculate the distance between the points with the min and max velocity based on the next tick being applied
            var nextDistance = minP.GetNextPosition().ManhattanDistance(maxP.GetNextPosition());

            // Once the next distance becomes greater than the latest distance
            // then the points will have crossed and started to move away from each other
            // break out of the loop before applying the next tick as the current state will show the message
            if (nextDistance > distance)
                break;

            foreach (var point in points)
                point.MoveNext();

            distance = nextDistance;
            ticks++;
        }

        Console.WriteLine("Part 1:\n");
        PrintMessage(points);
        Console.WriteLine($"\nPart 2: {ticks}");
    }

    private static void PrintMessage(List<Point> points)
    {
        var minX = points.Min(p => p.Position.X);
        var maxX = points.Max(p => p.Position.X);
        var minY = points.Min(p => p.Position.Y);
        var maxY = points.Max(p => p.Position.Y);

        for (int y = minY; y <= maxY; y++)
        {
            var sb = new StringBuilder();
            for (int x = minX; x <= maxX; x++)
                sb.Append(points.Any(p => p.Position == (x, y)) ? 'x' : ' ');

            Console.WriteLine(sb);
        }
    }

    private static Point CreatePoint(string line)
    {
        var split = line.Split(new[] { ", ", "=", "<", ">" }, StringSplitOptions.RemoveEmptyEntries);
        return new()
        {
            Position = (int.Parse(split[1]), int.Parse(split[2])),
            Velocity = (int.Parse(split[4]), int.Parse(split[5]))
        };
    }
}
