using UnityEngine;
using TMPro;

public class RadarElement : MonoBehaviour
{
    public float orbitRadius;
    private GameObject targetCell;

    void Start()
    {
        LeanTween.alpha(GetComponent<RectTransform>(), 0.5f, 2).setLoopPingPong();
        // LeanTween.scale(this.gameObject, this.transform.localScale * 0.8f, 2).setLoopPingPong();
    }

    void Update()
    {
        if (targetCell != null)
        {
            UpdatePosition();
        }
    }

    public void SetTargetCell(GameObject targetCell)
    {
        this.targetCell = targetCell;
        UpdatePosition();
    }

    public void SetAttackerCount(int count)
    {
        GetComponentInChildren<TextMeshProUGUI>().text = count + "x";
    }

    public void UpdatePosition()
    {
        Vector3 directionVector = targetCell.transform.position - transform.parent.position;
        if (directionVector.magnitude > orbitRadius)
        {
            directionVector = directionVector.normalized * orbitRadius;
        }
        transform.localPosition = directionVector;
        transform.localRotation = Quaternion.identity;
    }
}
