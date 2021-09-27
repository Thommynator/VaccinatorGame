using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{

    void Awake()
    {
        LeanTween.reset();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void LeaveGame()
    {
        Application.Quit();
    }

    public void StartTutorial()
    {
        SceneManager.LoadScene("TutorialScene");
    }
}
