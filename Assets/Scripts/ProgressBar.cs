using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    private Gradient gradient;

    private Slider slider;

    private Image fillImage;

    void Awake()
    {
        slider = GetComponent<Slider>();
        fillImage = transform.Find("Bar Fill").GetComponent<Image>();
    }

    public void SetValue(float newValue, float minValue, float maxValue)
    {
        slider.minValue = minValue;
        slider.maxValue = maxValue;
        slider.value = newValue;
        fillImage.color = gradient.Evaluate(slider.normalizedValue);
    }

}
