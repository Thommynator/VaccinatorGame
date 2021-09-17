using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager current;

    [SerializeField]
    private int waveDurationInSeconds;

    [SerializeField]
    private int wave = 1;


    void Start()
    {
        current = this;
        StartCoroutine(IncreaseWave());
    }

    private IEnumerator IncreaseWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(waveDurationInSeconds);
            wave++;
            GameEvents.current.IncreaseWave(wave);
        }
    }

    public int GetCurrentWave()
    {
        return wave;
    }


}
