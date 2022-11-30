using AdventOfCode.Tools;

namespace Day10;

public class Point
{
    public (int X, int Y) Position { get; set; }
    public (int X, int Y) Velocity { get; set; }

    public void MoveNext() => Position = GetNextPosition();
    public (int X, int Y) GetNextPosition() => (Position.X + Velocity.X, Position.Y + Velocity.Y);
    public int ManhattanDistance(Point other) => Position.ManhattanDistance(other.Position);
}
