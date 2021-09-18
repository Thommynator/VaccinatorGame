using UnityEngine;
using TMPro;
using System;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI attackerCountText;
    public TextMeshProUGUI waveCountText;

    void Start()
    {
        GameEvents.current.onIncreaseWave += UpdateWaveCountVisuals;
        GameEvents.current.onUpdateAttackerScore += UpdateAttackerCountVisuals;
        GameEvents.current.onUpdateMoneyScore += UpdateMoneyScoreVisuals;
        GameEvents.current.onUpdateSurviveTimeScore += UpdateSurviveTimeVisuals;

        UpdateMoneyScoreVisuals(0);
    }

    private void UpdateMoneyScoreVisuals(int money)
    {
        moneyText.text = money.ToString() + " $";
    }

    private void UpdateAttackerCountVisuals(int attackerCount)
    {
        attackerCountText.text = attackerCount.ToString();
    }

    private void UpdateWaveCountVisuals(int newWave)
    {
        waveCountText.text = "Wave: " + newWave;
    }

    private void UpdateSurviveTimeVisuals(float timeInSeconds)
    {
        timeText.text = GameManager.current.GetFormattedSurviveTime();
    }

    private void OnDestroy()
    {
        GameEvents.current.onIncreaseWave -= UpdateWaveCountVisuals;
        GameEvents.current.onIncreaseWave -= UpdateWaveCountVisuals;
        GameEvents.current.onUpdateAttackerScore -= UpdateAttackerCountVisuals;
        GameEvents.current.onUpdateMoneyScore -= UpdateMoneyScoreVisuals;
        GameEvents.current.onUpdateSurviveTimeScore -= UpdateSurviveTimeVisuals;
    }
}
