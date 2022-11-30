namespace Day07;

public class Elf
{
    private const int BASE_TASK_DURATION = 60 - 'A';
    public char CurrentTask { get; private set; }
    public int RemainingDuration { get; private set; }
    public bool HasTask => RemainingDuration > 0;

    public bool TryAssignNewTask(char task)
    {
        if (HasTask) return false;

        CurrentTask = task;
        RemainingDuration = task + BASE_TASK_DURATION;

        return true;
    }

    public bool Tick()
    {
        RemainingDuration--;
        return !HasTask;
    }
}
