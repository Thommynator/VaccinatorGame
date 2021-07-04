using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{

    public static GameEvents current;

    void Awake()
    {
        current = this;
    }

    public event Action<GameObject, GameObject> onAttackerAttachsToCell;
    public void AttackerAttachsToCell(GameObject attacker, GameObject cell)
    {
        onAttackerAttachsToCell?.Invoke(attacker, cell);
    }

    public event Action<GameObject, GameObject> onAttackerDetachesFromCell;
    public void AttackerDetachesFromCell(GameObject attacker, GameObject cell)
    {
        onAttackerDetachesFromCell?.Invoke(attacker, cell);
    }

    public event Action<GameObject> onAttackerDies;
    public void AttackerDies(GameObject attacker)
    {
        onAttackerDies?.Invoke(attacker);
    }




    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
