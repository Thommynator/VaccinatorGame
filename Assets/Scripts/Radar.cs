using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{

    public GameObject radarElementPrefab;
    private Dictionary<GameObject, int> attackedCells;
    private Dictionary<GameObject, GameObject> radarElements;

    void Start()
    {
        attackedCells = new Dictionary<GameObject, int>(); // cell to number of attackers
        radarElements = new Dictionary<GameObject, GameObject>(); // cell to radar element
        GameEvents.current.onAttackerAttachsToCell += AddCellAttacker;
        GameEvents.current.onAttackerDetachesFromCell += RemoveCellAttacker;
    }

    private void AddCellAttacker(GameObject attacker, GameObject cell)
    {
        attackedCells.TryGetValue(cell, out int numberOfAttackers);
        if (numberOfAttackers == 0)
        {
            GameObject radarElement = GameObject.Instantiate<GameObject>(radarElementPrefab, Vector3.zero, Quaternion.identity);
            radarElement.transform.SetParent(this.transform);
            radarElement.GetComponent<RadarElement>().SetTargetCell(cell);
            radarElements.Add(cell, radarElement);
        }

        numberOfAttackers++;
        radarElements[cell].GetComponent<RadarElement>().SetAttackerCount(numberOfAttackers);
        attackedCells[cell] = numberOfAttackers;
    }

    private void RemoveCellAttacker(GameObject attacker, GameObject cell)
    {
        attackedCells.TryGetValue(cell, out int numberOfAttackers);
        if (numberOfAttackers > 1)
        {
            numberOfAttackers--;
            attackedCells[cell] = numberOfAttackers;
            radarElements[cell].GetComponent<RadarElement>().SetAttackerCount(numberOfAttackers);
        }
        else
        {
            attackedCells.Remove(cell);
            if (radarElements.ContainsKey(cell))
            {
                Destroy(radarElements[cell]);
                radarElements.Remove(cell);
            }
        }
    }
}
