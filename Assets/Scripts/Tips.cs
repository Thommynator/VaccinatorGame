using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tips : MonoBehaviour
{
    public GameObject attackingVirus;
    private List<Transform> allTips = new List<Transform>();
    private int currentTip;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        foreach (Transform tip in this.transform)
        {
            allTips.Add(tip);
        }
        currentTip = 0;
        ShowTipAndHidePreviousTip();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void EnableEnemySpawner()
    {
        GameObject.Find("Enemy Spawner").GetComponent<FixedPositionSpawner>().enabled = true;
    }

    public void ActivateCellAttackingVirus()
    {
        attackingVirus.SetActive(true);
    }

    public void ShowNextTip()
    {
        audioSource.Play();
        currentTip++;
        ShowTipAndHidePreviousTip();
    }

    private void ShowTipAndHidePreviousTip()
    {
        if (currentTip > 0)
        {
            allTips[currentTip - 1].GetComponentInChildren<Tip>().Hide();
        }
        if (currentTip < allTips.Count)
        {
            allTips[currentTip].GetComponentInChildren<Tip>().Show();
        }
    }
}
