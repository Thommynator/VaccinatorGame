using UnityEngine;

public class FixedPositionSpawner : MonoBehaviour
{

    public GameObject objectToSpawn;

    public float spawnRatePerSecond;

    private float lastSpawnTime;

    void Start()
    {
        lastSpawnTime = Time.realtimeSinceStartup;
    }
    // Update is called once per frame
    void Update()
    {
        float intervalTime = 1.0f / spawnRatePerSecond;
        if (Time.realtimeSinceStartup - lastSpawnTime > intervalTime)
        {
            GameObject spawnedObject = GameObject.Instantiate<GameObject>(objectToSpawn, transform.position, Quaternion.identity);
            lastSpawnTime = Time.realtimeSinceStartup;

            if (spawnedObject.layer.Equals(LayerMask.NameToLayer("Attacker")))
            {
                GameEvents.current.IncreaseAttackerCount();
            }
        }
    }
}
