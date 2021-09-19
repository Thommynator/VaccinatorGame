using UnityEngine;

public abstract class MovementBehavior : MonoBehaviour
{
    protected Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        body.AddForce(ComputeMovementForce(), ForceMode2D.Impulse);
        transform.up = body.velocity;
    }

    public abstract Vector2 ComputeMovementForce();

}
