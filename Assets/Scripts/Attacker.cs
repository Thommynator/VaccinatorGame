﻿using UnityEngine;

public class Attacker : MonoBehaviour
{
    public float damage;
    public float attackCooldown;
    public GameObject explosionEffectPrefab;
    private float lastAttackTime;


    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onAttackerAttachsToCell += StopMoving;
        GameEvents.current.onAttackerDetachesFromCell += StartMoving;

        GetComponent<Hp>().dyingMethod = Die;
        lastAttackTime = Time.timeSinceLevelLoad;
    }


    public void AttackCell(GameObject cell)
    {
        LookAt(cell);
        if (Time.timeSinceLevelLoad - lastAttackTime > attackCooldown)
        {
            cell.GetComponent<Hp>().TakeDamage(damage);
            lastAttackTime = Time.timeSinceLevelLoad;
        }
    }

    public void Die()
    {
        GameEvents.current.IncreaseVisibleArea(1.0f);
        GameEvents.current.ShakeCamera(0.25f, 5);
        GameObject explosionFx = GameObject.Instantiate<GameObject>(explosionEffectPrefab, transform.position, Quaternion.identity);
        Destroy(explosionFx, 3);
        Destroy(this.gameObject);
    }

    private void StopMoving(GameObject attacker, GameObject cell)
    {
        if (attacker == this.gameObject)
        {
            GetComponent<RandomWalker>().enabled = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            LookAt(cell);
        }
    }

    private void StartMoving(GameObject attacker, GameObject cell)
    {
        if (attacker == this.gameObject)
        {
            GetComponent<RandomWalker>().enabled = true;
        }
    }

    private void LookAt(GameObject target)
    {
        transform.up = target.transform.position - transform.position;
    }

    private void OnDestroy()
    {
        GameEvents.current.AttackerDies(this.gameObject);
        GameEvents.current.onAttackerAttachsToCell -= StopMoving;
        GameEvents.current.onAttackerDetachesFromCell -= StartMoving;
    }
}
