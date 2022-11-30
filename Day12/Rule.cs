namespace Day12;

public class Rule
{
    public string Pattern { get; init; }
    public bool GrowsPlant { get; init; }

    public Rule(string line)
    {
        var split = line.Split(" => ");
        Pattern = split[0];
        GrowsPlant = split[1] == "#";
    }
}
