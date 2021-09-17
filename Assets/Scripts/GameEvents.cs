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

    public event Action<float> onIncreaseMoney;
    public void IncreaseMoney(float increase)
    {
        onIncreaseMoney?.Invoke(increase);
    }

    public event Action<float> onDecreaseMoney;
    public void DecreaseMoney(float decrease)
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

    public event Action pauseGame;
    public void PauseGame()
    {
        pauseGame?.Invoke();
    }

    public event Action resumeGame;
    public void ResumeGame()
    {
        resumeGame?.Invoke();
    }

    public event Action<float> shakeCamera;
    public void ShakeCamera(float duration)
    {
        shakeCamera?.Invoke(duration);
    }

    public event Action<int> increaseWave;
    public void IncreaseWave(int newWave)
    {
        increaseWave?.Invoke(newWave);
    }

}
