using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstAttack : MonoBehaviour
{

    public int numberOfBurstShotProjectiles;
    private float lastBurstAttackTime;


    void Start()
    {
        lastBurstAttackTime = Time.time - 1000; // subtraction to make sure that there is no cooldown at the start of the game
    }

    public bool Fire(Vector3 initialVelocity)
    {
        if (Time.time - lastBurstAttackTime <= ShopItems.current.GetValueOf(ItemName.BURST_ATTACK_COOLDOWN))
        {
            return false;
        }

        lastBurstAttackTime = Time.time;
        StartCoroutine(SpawnProjectiles(numberOfBurstShotProjectiles, initialVelocity));
        return true;
    }

    private IEnumerator SpawnProjectiles(int projectiles, Vector3 initialVelocity)
    {
        ProjectileSpawner projectileSpawner = GetComponent<ProjectileSpawner>();
        for (int i = 0; i < projectiles; i++)
        {
            projectileSpawner.Fire(transform.position, transform.position + transform.up, initialVelocity);
            yield return new WaitForEndOfFrame();
        }
    }
}
