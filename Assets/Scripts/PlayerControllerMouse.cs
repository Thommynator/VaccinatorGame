using UnityEngine;

public class PlayerControllerMouse : MonoBehaviour
{
    public float maxSpeed;
    private Rigidbody2D body;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        body = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Update()
    {
        Rotate();

        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<ProjectileSpawner>().Fire(transform.position, GetMouseWorldPosition());
        }
    }

    private void Move()
    {
        Vector2 movementForce = Vector2.zero;
        movementForce += new Vector2(Input.GetAxis("Horizontal"), 0);
        movementForce += new Vector2(0, Input.GetAxis("Vertical"));
        Vector2 offset = Vector2.ClampMagnitude(movementForce * maxSpeed, maxSpeed);
        Debug.DrawLine(transform.position, (Vector2)transform.position + offset * 10);
        body.MovePosition((Vector2)transform.position + offset);
    }

    private void Rotate()
    {
        Vector3 worldMousePosition = GetMouseWorldPosition();
        transform.up = worldMousePosition - transform.position;
        Debug.DrawLine(transform.position, worldMousePosition);
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 position = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0;
        return position;
    }

}
