using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class VisibleArea : MonoBehaviour
{
    public float distance;

    private float maxDistance;

    public float decreaseIntervalInSeconds;
    public float decreaseAmount;

    private GameObject spriteMaskObject;
    private Light2D light2D;

    void Start()
    {
        // subscribe to events
        GameEvents.current.onIncreaseVisibleArea += IncreaseVisableDistance;
        GameEvents.current.onDecreaseVisibleArea += DecreaseVisableDistance;

        maxDistance = distance;
        spriteMaskObject = GetComponentInChildren<SpriteMask>().gameObject;
        light2D = GetComponentInChildren<Light2D>();
        StartCoroutine(DecreaseVisibleArea());
    }


    private void IncreaseVisableDistance(float increase)
    {
        distance += increase;
        distance = Mathf.Clamp(distance, 0, maxDistance);
    }

    private void DecreaseVisableDistance(float decrease)
    {
        distance -= decrease;
        distance = Mathf.Clamp(distance, 0, maxDistance);
    }

    void Update()
    {
        spriteMaskObject.transform.localScale = Vector3.one * distance;
        light2D.pointLightOuterRadius = distance;
    }

    private IEnumerator DecreaseVisibleArea()
    {
        while (true)
        {
            DecreaseVisableDistance(decreaseAmount);
            yield return new WaitForSeconds(decreaseIntervalInSeconds);
        }
    }

    void OnDestroy()
    {
        GameEvents.current.onIncreaseVisibleArea -= IncreaseVisableDistance;
        GameEvents.current.onDecreaseVisibleArea -= DecreaseVisableDistance;
    }
}
