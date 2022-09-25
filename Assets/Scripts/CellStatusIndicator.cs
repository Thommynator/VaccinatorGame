using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CellStatusIndicator : MonoBehaviour
{
    private GameObject cellToCheck;

    private Image cellIcon;
    private TextMeshProUGUI numberOfAttackers;
    private TextMeshProUGUI relativeHealth;
    private int blinkTweenId;
    private bool isBlinking;

    // Start is called before the first frame update
    void Start()
    {
        cellIcon = this.transform.Find("Cell Icon").GetComponent<Image>();
        numberOfAttackers = this.transform.Find("Number Of Attackers").GetComponent<TextMeshProUGUI>();
        relativeHealth = this.transform.Find("Relative Health").GetComponent<TextMeshProUGUI>();
        blinkTweenId = LeanTween.alpha(cellIcon.rectTransform, 0.5f, 0.25f).setLoopPingPong().id;
        isBlinking = false;
        LeanTween.pause(blinkTweenId);

    }

    // Update is called once per frame
    void Update()
    {
        if (cellToCheck == null)
        {
            return;
        }

        cellIcon.sprite = cellToCheck.GetComponentInChildren<Cell>().GetCurrentStatusSprite();
        numberOfAttackers.text = cellToCheck.GetComponentInChildren<Cell>().numberOfAttackers.ToString();
        relativeHealth.text = ((int)cellToCheck.GetComponentInChildren<Hp>().GetHpPercentage()).ToString() + "%";

        HandleCellIconBlinking();

    }

    private void HandleCellIconBlinking()
    {
        Cell.CellStatus cellStatus = cellToCheck.GetComponentInChildren<Cell>().cellStatus;

        if (cellStatus == Cell.CellStatus.ATTACKED && !isBlinking)
        {
            LeanTween.resume(blinkTweenId);
            isBlinking = true;
        }
        else if ((cellStatus == Cell.CellStatus.HEALTHY || cellStatus == Cell.CellStatus.INFECTED) && isBlinking)
        {
            LeanTween.pause(blinkTweenId);
            LeanTween.alpha(cellIcon.rectTransform, 1, 0.1f);
            isBlinking = false;
        }
    }

    public void SetCellToCheck(GameObject cell)
    {
        this.cellToCheck = cell;
    }

}
