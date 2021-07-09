using UnityEngine;

public class PlayerControllerKeyboard : MonoBehaviour
{
    public float maxSpeed;
    public float maxRotation;
    private Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
        Rotate();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<ProjectileSpawner>().Fire(transform.position, transform.position + transform.up);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            GetComponent<DashAttack>().Dash(1);
        }
    }

    private void Move()
    {
        // body.MovePosition(transform.position + transform.up * Mathf.Clamp(Input.GetAxis("Vertical") * maxSpeed, -maxSpeed, maxSpeed));
        body.AddForce(transform.up * Mathf.Clamp(Input.GetAxis("Vertical") * maxSpeed, -maxSpeed, maxSpeed));
    }

    private void Rotate()
    {
        body.SetRotation(body.rotation + Mathf.Clamp(-Input.GetAxis("Horizontal") * maxRotation, -maxRotation, maxRotation));
        // body.AddTorque(Mathf.Clamp(-Input.GetAxis("Horizontal") * maxRotation, -maxRotation, maxRotation));
    }



}
