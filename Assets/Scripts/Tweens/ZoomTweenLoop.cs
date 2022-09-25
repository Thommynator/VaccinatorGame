using UnityEngine;

public class ZoomTweenLoop : MonoBehaviour
{

    public Vector3 zoomTo;
    public float iterationTimeInSeconds;
    public LeanTweenType easeType;

    void Start()
    {
        LeanTween
            .scale(this.gameObject, zoomTo, iterationTimeInSeconds)
            .setEase(easeType)
            .setIgnoreTimeScale(true)
            .setLoopPingPong();
    }

}
