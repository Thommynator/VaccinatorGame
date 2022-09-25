using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{

    public TextMeshProUGUI waveText;
    public TextMeshProUGUI timeText;

    void Start()
    {
        GameEvents.current.onGameOver += UpdateGameOverScreen;
    }

    private void UpdateGameOverScreen()
    {
        // wave
        int waves = WaveManager.current.GetCurrentWave();
        string waveWord = waves == 1 ? " wave" : " waves";
        waveText.text = waves + waveWord;

        // survive time
        timeText.text = "in " + GameManager.current.GetFormattedSurviveTime();
    }

    void OnDestroy()
    {
        GameEvents.current.onGameOver -= UpdateGameOverScreen;
    }
}
