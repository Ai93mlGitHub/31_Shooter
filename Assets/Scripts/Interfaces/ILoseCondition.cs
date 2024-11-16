public interface ILoseCondition
{
    event System.Action OnLose;
    void UpdateCondition();
}