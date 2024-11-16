public interface IWinCondition
{
    event System.Action OnWin;
    void UpdateCondition();
}