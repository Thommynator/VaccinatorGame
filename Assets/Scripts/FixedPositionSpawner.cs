using UnityEngine;

public class FixedPositionSpawner : MonoBehaviour
{

    public GameObject objectToSpawn;

    public AnimationCurve spawnIntervalTimeInSec;

    private float lastSpawnTime;

    void Start()
    {
        lastSpawnTime = Time.timeSinceLevelLoad  + Random.Range(0, SpawnIntervalTime());
    }
    // Update is called once per frame
    void Update()
    {
        float intervalTime = SpawnIntervalTime();
        if (Time.timeSinceLevelLoad  - lastSpawnTime > intervalTime)
        {
            GameObject spawnedObject = GameObject.Instantiate<GameObject>(objectToSpawn, transform.position, Quaternion.identity);
            spawnedObject.transform.SetParent(this.transform);
            lastSpawnTime = Time.timeSinceLevelLoad ;

            if (spawnedObject.layer.Equals(LayerMask.NameToLayer("Attacker")))
            {
                GameEvents.current.IncreaseAttackerCount();
            }
        }
    }

    private float SpawnIntervalTime()
    {
        return spawnIntervalTimeInSec.Evaluate(WaveManager.current.GetCurrentWave());
    }
}
