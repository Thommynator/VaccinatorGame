using System.Collections;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{

    private CinemachineVirtualCamera cam;

    void Start()
    {
        GameEvents.current.onShakeCamera += ShakeCamera;

        cam = GetComponent<CinemachineVirtualCamera>();
    }

    private void ShakeCamera(float duration, float strength)
    {
        StartCoroutine(Shake(duration, strength));
    }

    private IEnumerator Shake(float duration, float strength)
    {
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = strength;
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0.2f;
        yield return new WaitForSeconds(duration);
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
    }

    void OnDestroy()
    {
        GameEvents.current.onShakeCamera -= ShakeCamera;
    }
}
