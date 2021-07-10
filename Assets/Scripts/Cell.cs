﻿using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Experimental.Rendering.Universal;

public class Cell : MonoBehaviour
{
    public HashSet<GameObject> attackedBy = new HashSet<GameObject>();
    public int numberOfAttackers;
    public CellStatus cellStatus;
    public float hpRegenarationPerSecond;
    public GameObject enemySpawnerPrefab;
    public Sprite[] cellStatusSprites;
    public TextMeshProUGUI hpText;
    public Color[] backgroundLightColor;
    private Light2D backgroundLight;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onAttackerAttachsToCell += AddAttacker;
        GameEvents.current.onAttackerDetachesFromCell += RemoveAttacker;
        GameEvents.current.onAttackerDies += (GameObject attacker) => { if (this != null) RemoveAttacker(attacker, this.gameObject); };
        numberOfAttackers = 0;
        cellStatus = CellStatus.HEALTHY;
        backgroundLight = transform.Find("Background Light").GetComponent<Light2D>();

        StartCoroutine(RecoverHealth(1));
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject attacker in attackedBy)
        {
            attacker.GetComponent<Attacker>().AttackCell(this.gameObject);
        }
        SetCellStatus();
        hpText.text = GetComponent<Hp>().GetHp().ToString();
    }

    private void SetCellStatus()
    {
        float hp = GetComponent<Hp>().GetHp();
        if (GetNumberOfAttackers() == 0 && hp > 0)
        {
            cellStatus = CellStatus.HEALTHY;
            backgroundLight.color = backgroundLightColor[0];
            GetComponentInChildren<SpriteRenderer>().sprite = cellStatusSprites[0];
        }
        else if (GetNumberOfAttackers() > 0 && hp > 0)
        {
            cellStatus = CellStatus.ATTACKED;
            backgroundLight.color = backgroundLightColor[1];
            GetComponentInChildren<SpriteRenderer>().sprite = cellStatusSprites[1];
        }
        else if (hp <= 0 && cellStatus != CellStatus.INFECTED)
        {
            ConvertToInfectedCell();
            backgroundLight.color = backgroundLightColor[2];
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

    private IEnumerator RecoverHealth(float waitTime)
    {
        while (true)
        {
            if (cellStatus == CellStatus.HEALTHY)
            {
                GetComponent<Hp>().IncreaseHp(hpRegenarationPerSecond);
            }
            yield return new WaitForSeconds(waitTime);
        }
    }

    private void AddAttacker(GameObject attacker, GameObject cell)
    {
        if (cell != this.gameObject)
        {
            return;
        }

        attackedBy.Add(attacker);
        numberOfAttackers = GetNumberOfAttackers();

        FixedJoint2D joint = attacker.gameObject.AddComponent<FixedJoint2D>();
        joint.connectedBody = GetComponent<Rigidbody2D>();
        joint.autoConfigureConnectedAnchor = false;

    }

    private void RemoveAttacker(GameObject attacker, GameObject cell)
    {
        if (cell != this.gameObject)
        {
            return;
        }
        if (attackedBy.Remove(attacker))
        {
            numberOfAttackers = GetNumberOfAttackers();
            Destroy(attacker.gameObject.GetComponent<FixedJoint2D>());
        }
    }

    private int GetNumberOfAttackers()
    {
        return attackedBy.Count;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((cellStatus == CellStatus.HEALTHY || cellStatus == CellStatus.ATTACKED)
            && collision.gameObject.layer.Equals(LayerMask.NameToLayer("Attacker")))
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
