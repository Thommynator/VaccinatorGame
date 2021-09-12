using System.Collections;
using UnityEngine;

public class PlayerControllerKeyboard : MonoBehaviour
{
    public float maxSpeed;
    public float maxRotation;
    private Rigidbody2D body;
    private bool canDoAction;

    void Start()
    {
        GameEvents.current.resumeGame += SetCanDoAction;
        GameEvents.current.pauseGame += SetCanNotDoAction;

        canDoAction = true;
        body = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!canDoAction)
        {
            return;
        }
        Move();
        Rotate();
    }

    void Update()
    {
        if (!canDoAction)
        {
            return;
        }

        // single precise shot
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            GetComponent<ProjectileSpawner>().Fire(transform.position, transform.position + transform.up, 2 * body.velocity);
        }

        // burst attack
        else if (Input.GetMouseButton(1))
        {
            GetComponent<BurstAttack>().Fire(body.velocity);
        }

        // dash attack
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

    private void SetCanDoAction()
    {
        canDoAction = true;
    }

    private void SetCanNotDoAction()
    {
        canDoAction = false;
    }

    void OnDestroy()
    {
        GameEvents.current.resumeGame -= SetCanDoAction;
        GameEvents.current.pauseGame -= SetCanNotDoAction;
    }

}
