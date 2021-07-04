using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class VisibleArea : MonoBehaviour
{

    public float distance;

    private GameObject spriteMaskObject;
    private Light2D light2D;

    void Start()
    {
        spriteMaskObject = GetComponentInChildren<SpriteMask>().gameObject;
        light2D = GetComponentInChildren<Light2D>();
    }

    void Update()
    {
        spriteMaskObject.transform.localScale = Vector3.one * distance;
        light2D.pointLightOuterRadius = distance;
    }
}
