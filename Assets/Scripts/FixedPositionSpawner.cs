using UnityEngine;

public class FixedPositionSpawner : MonoBehaviour
{

    public GameObject objectToSpawn;

    public float spawnRatePerSecond;

    private float lastSpawnTime;

    void Start()
    {
        lastSpawnTime = Time.time + Random.Range(0, 1.0f / spawnRatePerSecond);
    }
    // Update is called once per frame
    void Update()
    {
        float intervalTime = 1.0f / spawnRatePerSecond;
        if (Time.time - lastSpawnTime > intervalTime)
        {
            GameObject spawnedObject = GameObject.Instantiate<GameObject>(objectToSpawn, transform.position, Quaternion.identity);
            spawnedObject.transform.SetParent(this.transform);
            lastSpawnTime = Time.time;

            if (spawnedObject.layer.Equals(LayerMask.NameToLayer("Attacker")))
            {
                GameEvents.current.IncreaseAttackerCount();
            }
        }
    }
}
