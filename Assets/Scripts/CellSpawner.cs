using UnityEngine;

public class CellSpawner : MonoBehaviour
{
    public GameObject cellPrefab;
    public int numberOfCells;
    public int maxDimension;

    void Start()
    {
        for (int i = 0; i < numberOfCells; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-maxDimension, maxDimension), Random.Range(-maxDimension, maxDimension));
            Quaternion randomRotation = Quaternion.AngleAxis(Random.Range(0f, 360.0f), Vector3.forward);
            GameObject newCell = GameObject.Instantiate<GameObject>(cellPrefab, randomPosition, Quaternion.identity);
            newCell.transform.Find("Cell").transform.rotation = randomRotation;
            newCell.transform.parent = this.gameObject.transform;
        }
    }

    void Update()
    {

    }
}
