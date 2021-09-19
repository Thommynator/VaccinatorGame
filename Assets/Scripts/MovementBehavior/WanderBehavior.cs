using UnityEngine;

/**
* Reynolds wander steering behavior: https://gamedevelopment.tutsplus.com/tutorials/understanding-steering-behaviors-wander--gamedev-1624
*/
public class WanderBehavior : MovementBehavior
{
    public float maxDistance;
    private float wanderAngleInDeg = 0;
    private float maxAngleChangeInDeg = 10;
    private float circleDistance = 3;
    private float circleRadius = 5f;

    public override Vector2 ComputeMovementForce()
    {

        Vector2 circlePosition = transform.position + base.body.transform.up * circleDistance;
        Debug.DrawLine(transform.position, circlePosition, Color.blue);


        Vector2 innerCircleVector = Quaternion.AngleAxis(wanderAngleInDeg, Vector3.forward) * Vector2.up * circleRadius;
        Vector2 positionOnCircle = circlePosition + innerCircleVector;
        Debug.DrawLine(circlePosition, positionOnCircle, Color.cyan);
        wanderAngleInDeg += Random.Range(-maxAngleChangeInDeg, maxAngleChangeInDeg);
        Vector2 direction = positionOnCircle - (Vector2)transform.position;

        return direction.normalized * maxDistance;
    }

}
