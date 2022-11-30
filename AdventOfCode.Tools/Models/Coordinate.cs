namespace AdventOfCode.Tools;

public class Coordinate
{
    private static int _nextId = 1;

    public int Id { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

    public Coordinate(int x, int y)
    {
        X = x;
        Y = y;
        Id = _nextId++;
    }

    public Coordinate(int id, int x, int y)
    {
        Id = id;
        X = x;
        Y = y;
    }

    public Coordinate(string x, string y)
    {
        X = int.Parse(x);
        Y = int.Parse(y);
        Id = _nextId++;
    }

    public int ManhattanDistance(int x, int y) => (X, Y).ManhattanDistance((x, y));
}
