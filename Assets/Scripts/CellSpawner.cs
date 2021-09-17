using UnityEngine;

public class CellSpawner : MonoBehaviour
{
    public GameObject cellPrefab;
    public int numberOfCells;
    public float clearRadiusAroundCell;
    public Transform bottomLeftCorner;
    public Transform topRightCorner;

    void Start()
    {

        for (int i = 0; i < numberOfCells; i++)
        {
            Quaternion randomRotation = Quaternion.AngleAxis(Random.Range(0f, 360.0f), Vector3.forward);
            GameObject newCell = GameObject.Instantiate<GameObject>(cellPrefab, FindNonOverlappingPosition(), Quaternion.identity);
            newCell.transform.Find("Cell").transform.rotation = randomRotation;
            newCell.transform.parent = this.gameObject.transform;
        }
    }

    private Vector3 CreateRandomPosition()
    {
        float minX = bottomLeftCorner.position.x;
        float minY = bottomLeftCorner.position.y;
        float maxX = topRightCorner.position.x;
        float maxY = topRightCorner.position.y;
        return new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    private Vector3 FindNonOverlappingPosition()
    {
        Vector3 randomPosition = CreateRandomPosition();
        int maxIterations = 10000;
        for (int i = 0; i < maxIterations; i++)
        {
            Collider2D collider = Physics2D.OverlapCircle(randomPosition, clearRadiusAroundCell);

            if (collider == null)
            {
                break;
            }
            randomPosition = CreateRandomPosition();
            if (i == maxIterations - 1)
            {
                Debug.Log("Couldn't find proper position for cell.");
            }
        }
        return randomPosition;
    }

    void Update()
    {

    }

}
