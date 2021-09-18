using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{

    private CanvasGroup canvasGroup;

    public GameObject enterShopButton;

    void Start()
    {
        canvasGroup = GetComponentInChildren<CanvasGroup>();
        Hide();
    }

    void Update()
    {

    }

    public void Hide()
    {
        GameEvents.current.ResumeGame();
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;

        enterShopButton.SetActive(true);
    }

    public void Show()
    {
        Time.timeScale = 0;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        enterShopButton.SetActive(false);
    }
}
