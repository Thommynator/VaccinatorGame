using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System;

public class Tip : MonoBehaviour
{
    [Header("Content")]
    public string title;
    public string description;

    [Header("Textfields")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;

    [Header("Trigger")]
    public UnityEvent trigger;

    private float tweenDuration = 0.75f;

    void Start()
    {
        this.transform.localScale = Vector3.zero;
        this.titleText.text = title;
        this.descriptionText.text = description.Replace("\\n", "\n");
    }

    public void Show()
    {
        LeanTween.scale(this.gameObject, Vector3.one, tweenDuration).setEaseOutBack();
    }

    public void Hide()
    {
        LeanTween.scale(this.gameObject, Vector3.zero, tweenDuration).setEaseOutCubic();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GetComponent<BoxCollider2D>().enabled = false;
            trigger?.Invoke();
        }
    }
}
