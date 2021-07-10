using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{

    public GameObject projectilePrefab;

    public void Fire(Vector3 spawnPosition, Vector3 target, Vector3 initialVelocity)
    {
        GameObject rocket = GameObject.Instantiate<GameObject>(projectilePrefab, spawnPosition, Quaternion.identity);
        rocket.GetComponent<Rigidbody2D>().velocity = initialVelocity;
        rocket.transform.up = target - transform.position;
    }
}
