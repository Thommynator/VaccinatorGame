using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstShot : MonoBehaviour
{

    public int numberOfBurstShotProjectiles;
    public float burstShotCoolDownDuration;
    private float lastBurstShotTime;


    void Start()
    {
        lastBurstShotTime = Time.realtimeSinceStartup;
    }


    public void Fire(Vector3 initialVelocity)
    {
        if (Time.realtimeSinceStartup - lastBurstShotTime > burstShotCoolDownDuration)
        {
            lastBurstShotTime = Time.realtimeSinceStartup;
            StartCoroutine(SpawnProjectiles(numberOfBurstShotProjectiles, initialVelocity));
        }
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
