namespace Day04;

public enum ActivityType
{
    None,
    WakesUp,
    FallsAsleep,
    NewGuardOnDuty
}

public class GuardActivity
{
    public DateTime ActivityDateTime { get; set; }
    public ActivityType ActivityType { get; set; }
    public int Id { get; set; } = 0;
    public string Line { get; set; }

    public GuardActivity(string line)
    {
        Line = line;
        var split = line.Split(new[] { '[', ']', '-', ' ', '#', ':' }, StringSplitOptions.RemoveEmptyEntries);
        ActivityDateTime = new DateTime(int.Parse(split[0]), int.Parse(split[1]), int.Parse(split[2]), int.Parse(split[3]), int.Parse(split[4]), 0);
        ActivityType = split[5] switch
        {
            "wakes" => ActivityType.WakesUp,
            "falls" => ActivityType.FallsAsleep,
            "Guard" => ActivityType.NewGuardOnDuty,
            _ => ActivityType.None,
        };
        if (ActivityType == ActivityType.NewGuardOnDuty) Id = int.Parse(split[6]);
    }
}
