using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{

    public GameObject projectilePrefab;

    public void Fire(Vector3 spawnPosition, Vector3 target)
    {
        GameObject rocket = GameObject.Instantiate<GameObject>(projectilePrefab, spawnPosition, Quaternion.identity);
        rocket.transform.up = target - transform.position;
    }
}
