namespace Day04;

public class GuardDuty
{
    public int Id { get; set; } = 0;
    public List<int> AsleepMinutes { get; set; } = new ();
    
    public GuardDuty(int id)
    {
        Id = id;
    }

    public void RecordTimeAsleep(int fallAsleep, int wakeUp)
    {
        for (int i = fallAsleep; i < wakeUp; i++)
            AsleepMinutes.Add(i);
    }
}
