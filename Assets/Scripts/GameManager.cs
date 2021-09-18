using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager current;

    [Header("Score")]
    [SerializeField]
    private int money;
    private float timeInSeconds;
    private int attackerCount;

    [Header("Canvas")]
    public GameObject hud;
    public GameObject gameOverScreen;
    public GameObject shop;
    public GameObject pauseScreen;


    [Header("Other")]
    public GameObject player;

    private GameObject cellsParentGameObject;

    private bool isPaused;

    void Awake()
    {
        GameEvents.current.onIncreaseMoney += IncreaseMoney;
        GameEvents.current.onDecreaseMoney += DecreaseMoney;
        GameEvents.current.onIncreaseAttackerCount += IncreaseAttackerCount;
        GameEvents.current.onDecreaseAttackerCount += DecreaseAttackerCount;
        GameEvents.current.onPauseGame += PauseGame;
        GameEvents.current.onResumeGame += ResumeGame;

        current = this;

        cellsParentGameObject = GameObject.Find("Cells");
    }

    void Start()
    {
        attackerCount = 0;
        timeInSeconds = Time.timeSinceLevelLoad;
        isPaused = false;
        GameEvents.current.ResumeGame();
        StartCoroutine(IncreaseTimer());
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                GameEvents.current.ResumeGame();
            }
            else
            {
                GameEvents.current.PauseGame();
            }
        }
        HandleGameOver();
    }

    private void PauseGame()
    {
        Time.timeScale = 0.0f;
        isPaused = true;
        pauseScreen.SetActive(true);
    }

    private void ResumeGame()
    {
        Time.timeScale = 1.0f;
        isPaused = false;
        pauseScreen.SetActive(false);
    }

    private void HandleGameOver()
    {
        if (IsGameOver())
        {
            PauseGame();
            hud.SetActive(false);
            shop.SetActive(false);
            player.SetActive(false);
            gameOverScreen.SetActive(true);
            GameEvents.current.GameOver();
        }
        else
        {
            hud.SetActive(true);
            shop.SetActive(true);
            player.SetActive(true);
            gameOverScreen.SetActive(false);
        }
    }

    private bool IsGameOver()
    {
        foreach (Transform cellWrapper in cellsParentGameObject.transform)
        {
            if (!cellWrapper.GetComponentInChildren<Cell>().cellStatus.Equals(Cell.CellStatus.INFECTED))
            {
                return false;
            }
        }
        return true;
    }

    public float GetSurviveTime()
    {
        return timeInSeconds;
    }

    public String GetFormattedSurviveTime()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(GetSurviveTime());
        // 0:00 --> first number = index, second number = formatting (https://tinyurl.com/3fd6w7ey)
        return string.Format("{0:00}:{1:00}:{2:0}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds / 100);
    }

    private IEnumerator IncreaseTimer()
    {
        while (true)
        {
            this.timeInSeconds = Time.timeSinceLevelLoad;
            GameEvents.current.UpdateSurviveTimeScore(timeInSeconds);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public int GetMoney()
    {
        return money;
    }

    private void IncreaseMoney(int increase)
    {
        money += increase;
        GameEvents.current.UpdateMoneyScore(money);
    }
    private void DecreaseMoney(int decrease)
    {
        money -= decrease;
        GameEvents.current.UpdateMoneyScore(money);
    }

    public int GetAttackerCount()
    {
        return attackerCount;
    }

    private void IncreaseAttackerCount()
    {
        attackerCount += 1;
        GameEvents.current.UpdateAttackerScore(attackerCount);
    }
    private void DecreaseAttackerCount()
    {
        attackerCount -= 1;
        attackerCount = Mathf.Max(attackerCount, 0);
        GameEvents.current.UpdateAttackerScore(attackerCount);
    }

    public void Restart()
    {
        GetComponent<AudioSource>().Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        GetComponent<AudioSource>().Play();
        Application.Quit();
    }

    void OnDestroy()
    {
        GameEvents.current.onIncreaseMoney -= IncreaseMoney;
        GameEvents.current.onDecreaseMoney -= DecreaseMoney;
        GameEvents.current.onIncreaseAttackerCount -= IncreaseAttackerCount;
        GameEvents.current.onDecreaseAttackerCount -= DecreaseAttackerCount;
        GameEvents.current.onPauseGame -= PauseGame;
        GameEvents.current.onResumeGame -= ResumeGame;
    }
}
