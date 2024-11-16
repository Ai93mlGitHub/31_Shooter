using UnityEngine;

public class SurviveForTimeCondition : IWinCondition
{
    public event System.Action OnWin;

    private float _timeToSurvive = 10f;
    
    private float _timeSurvived = 0f;

    public void UpdateCondition()
    {
        _timeSurvived += Time.deltaTime;
        if (_timeSurvived >= _timeToSurvive)
        {
            OnWin?.Invoke();
        }
        Debug.Log($"Time: {_timeSurvived}");
    }
}