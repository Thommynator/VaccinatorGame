using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{

    public static GameEvents current;

    void Awake()
    {
        current = this;
        LeanTween.reset();
    }

    public event Action<GameObject, GameObject> onAttackerAttachsToCell;
    public void AttackerAttachsToCell(GameObject attacker, GameObject cell)
    {
        onAttackerAttachsToCell?.Invoke(attacker, cell);
    }

    public event Action<GameObject, GameObject> onAttackerDetachesFromCell;
    public void AttackerDetachesFromCell(GameObject attacker, GameObject cell)
    {
        onAttackerDetachesFromCell?.Invoke(attacker, cell);
    }

    public event Action<GameObject> onAttackerDies;
    public void AttackerDies(GameObject attacker)
    {
        onAttackerDies?.Invoke(attacker);
        onDecreaseAttackerCount?.Invoke();
    }

    public event Action<float> onIncreaseVisibleArea;
    public void IncreaseVisibleArea(float increment)
    {
        onIncreaseVisibleArea?.Invoke(increment);
    }

    public event Action<float> onDecreaseVisibleArea;
    public void DecreaseVisibleArea(float decrement)
    {
        onDecreaseVisibleArea?.Invoke(decrement);
    }

    public event Action<int> onIncreaseMoney;
    public void IncreaseMoney(int increase)
    {
        onIncreaseMoney?.Invoke(increase);
    }

    public event Action<int> onDecreaseMoney;
    public void DecreaseMoney(int decrease)
    {
        onDecreaseMoney?.Invoke(decrease);
    }

    public event Action onIncreaseAttackerCount;
    public void IncreaseAttackerCount()
    {
        onIncreaseAttackerCount?.Invoke();
    }

    public event Action onDecreaseAttackerCount;
    public void DecreaseAttackerCount()
    {
        onDecreaseAttackerCount?.Invoke();
    }

    public event Action<int> onIncreaseWave;
    public void IncreaseWave(int newWave)
    {
        onIncreaseWave?.Invoke(newWave);
    }

    public event Action<int> onUpdateAttackerScore;
    public void UpdateAttackerScore(int numberOfAttackers)
    {
        onUpdateAttackerScore?.Invoke(numberOfAttackers);
    }

    public event Action<int> onUpdateMoneyScore;
    public void UpdateMoneyScore(int money)
    {
        onUpdateMoneyScore?.Invoke(money);
    }

    public event Action<float> onUpdateSurviveTimeScore;
    public void UpdateSurviveTimeScore(float time)
    {
        onUpdateSurviveTimeScore?.Invoke(time);
    }

    public event Action onPauseGame;
    public void PauseGame()
    {
        onPauseGame?.Invoke();
    }

    public event Action onShowPauseScreen;
    public void ShowPauseScreen()
    {
        PauseGame();
        onShowPauseScreen?.Invoke();
    }

    public event Action onResumeGame;
    public void ResumeGame()
    {
        onResumeGame?.Invoke();
    }

    public event Action onGameOver;
    public void GameOver()
    {
        onGameOver?.Invoke();
    }

    public event Action<float, float> onShakeCamera;
    public void ShakeCamera(float duration, float strength)
    {
        onShakeCamera?.Invoke(duration, strength);
    }

}
