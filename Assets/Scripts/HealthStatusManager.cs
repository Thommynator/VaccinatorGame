using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthStatusManager : MonoBehaviour
{

    public GameObject indicatorPrefab;

    [SerializeField]
    private List<GameObject> cells;

    // Start is called before the first frame update
    void Start()
    {
        cells = FindAllCells();
        CreateStatusIndicators();
    }

    // Update is called once per frame
    void Update()
    {

    }

    List<GameObject> FindAllCells()
    {
        List<GameObject> cells = new List<GameObject>();
        GameObject cellsParent = GameObject.Find("Cells");
        foreach (Transform child in cellsParent.transform)
        {
            cells.Add(child.gameObject);
        }
        return cells;
    }

    void CreateStatusIndicators()
    {
        for (int i = 0; i < cells.Count; i++)
        {
            GameObject indicator = Instantiate<GameObject>(indicatorPrefab);
            indicator.transform.SetParent(this.gameObject.transform);
            indicator.transform.localScale = Vector3.one;
            indicator.transform.localPosition = new Vector2(50, 50 + 250 * i);
            indicator.GetComponent<CellStatusIndicator>().SetCellToCheck(cells[i]);
        }
    }
}
