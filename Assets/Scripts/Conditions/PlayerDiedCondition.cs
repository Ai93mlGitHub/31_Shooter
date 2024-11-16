public class PlayerDiedCondition : ILoseCondition
{
    public event System.Action OnLose;

    public void RegisterPlayerDied()
    {
        OnLose?.Invoke();
    }

    public void UpdateCondition() { }
}