using UnityEngine;

public class RotateTweenLoop : MonoBehaviour
{

    public float rotationAngle;
    public float iterationTimeInSeconds;
    public LeanTweenType easeType;

    void Start()
    {
        LeanTween
            .rotateZ(this.gameObject, rotationAngle, iterationTimeInSeconds)
            .setEase(easeType)
            .setIgnoreTimeScale(true)
            .setLoopPingPong();
    }

}
