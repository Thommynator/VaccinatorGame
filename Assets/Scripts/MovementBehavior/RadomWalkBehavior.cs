using UnityEngine;

public class RadomWalkBehavior : MovementBehavior
{
    public float maxDistance;

    public override Vector2 ComputeMovementForce()
    {
        return new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized * maxDistance;
    }

}
