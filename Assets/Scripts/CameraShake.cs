using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{

    private CinemachineVirtualCamera cam;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.shakeCamera += (duration) => StartCoroutine(Shake(duration));

        cam = GetComponent<CinemachineVirtualCamera>();
    }

    public IEnumerator Shake(float duration)
    {
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 2;
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0.2f;
        yield return new WaitForSeconds(duration);
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
    }


    // Update is called once per frame
    void Update()
    {

    }
}
