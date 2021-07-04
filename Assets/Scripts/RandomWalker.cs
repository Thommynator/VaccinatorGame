using UnityEngine;

public class RandomWalker : MonoBehaviour
{

    public float maxDistance;

    private Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        Vector2 randomOffset = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized * maxDistance;
        body.AddForce(randomOffset, ForceMode2D.Impulse);
        transform.up = body.velocity;
    }
}
