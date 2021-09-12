using System.Collections;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{

    private CinemachineVirtualCamera cam;

    void Start()
    {
        GameEvents.current.shakeCamera += ShakeCamera;

        cam = GetComponent<CinemachineVirtualCamera>();
    }

    private void ShakeCamera(float duration)
    {
        StartCoroutine(Shake(duration));
    }

    private IEnumerator Shake(float duration)
    {
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 2;
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0.2f;
        yield return new WaitForSeconds(duration);
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
    }

    void OnDestroy()
    {
        GameEvents.current.shakeCamera -= ShakeCamera;
    }
}
