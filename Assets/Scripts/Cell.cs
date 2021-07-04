using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public HashSet<GameObject> attackedBy = new HashSet<GameObject>();
    public int numberOfAttackers;
    public CellStatus cellStatus;
    public float hp;
    public GameObject enemySpawnerPrefab;

    public Sprite[] cellStatusSprites;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onAttackerAttachsToCell += AddAttacker;
        GameEvents.current.onAttackerDetachesFromCell += RemoveAttacker;
        GameEvents.current.onAttackerDies += (GameObject attacker) => { if (this != null) RemoveAttacker(attacker, this.gameObject); };
        numberOfAttackers = 0;
        cellStatus = CellStatus.HEALTHY;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject attacker in attackedBy)
        {
            attacker.GetComponent<Attacker>().AttackCell(this.gameObject);
        }

        SetCellStatus();
    }

    private void SetCellStatus()
    {
        if (GetNumberOfAttackers() == 0 && hp > 0)
        {
            cellStatus = CellStatus.HEALTHY;
            GetComponentInChildren<SpriteRenderer>().sprite = cellStatusSprites[0];
        }
        else if (GetNumberOfAttackers() > 0 && hp > 0)
        {
            cellStatus = CellStatus.ATTACKED;
            GetComponentInChildren<SpriteRenderer>().sprite = cellStatusSprites[1];
        }
        else if (hp <= 0 && cellStatus != CellStatus.INFECTED)
        {
            ConvertToInfectedCell();
            GetComponentInChildren<SpriteRenderer>().sprite = cellStatusSprites[2];
        }
    }

    private void ConvertToInfectedCell()
    {
        cellStatus = CellStatus.INFECTED;

        GameObject[] copy = new GameObject[attackedBy.Count];
        attackedBy.CopyTo(copy);

        foreach (GameObject attacker in copy)
        {
            GameEvents.current.AttackerDetachesFromCell(attacker, this.gameObject);
        }

        GameObject enemySpawner = GameObject.Instantiate<GameObject>(enemySpawnerPrefab, transform.position, Quaternion.identity);
        enemySpawner.transform.SetParent(this.transform);
    }

    private void AddAttacker(GameObject attacker, GameObject cell)
    {
        if (cell == this.gameObject)
        {
            attackedBy.Add(attacker);
            numberOfAttackers = GetNumberOfAttackers();

            FixedJoint2D joint = attacker.gameObject.AddComponent<FixedJoint2D>();
            joint.connectedBody = GetComponent<Rigidbody2D>();
            joint.autoConfigureConnectedAnchor = false;
        }
    }

    private void RemoveAttacker(GameObject attacker, GameObject cell)
    {
        if (cell == this.gameObject)
        {
            if (attackedBy.Remove(attacker))
            {
                numberOfAttackers = GetNumberOfAttackers();
                Destroy(attacker.gameObject.GetComponent<FixedJoint2D>());
            }
        }
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
    }

    private int GetNumberOfAttackers()
    {
        return attackedBy.Count;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((cellStatus == CellStatus.HEALTHY || cellStatus == CellStatus.ATTACKED) && collision.gameObject.tag == "Virus")
        {
            GameEvents.current.AttackerAttachsToCell(collision.gameObject, this.gameObject);
        }
    }

    void OnDestroy()
    {
        GameEvents.current.onAttackerAttachsToCell -= AddAttacker;
        GameEvents.current.onAttackerDetachesFromCell -= RemoveAttacker;
    }

    public enum CellStatus
    {
        HEALTHY, ATTACKED, INFECTED
    }
}
