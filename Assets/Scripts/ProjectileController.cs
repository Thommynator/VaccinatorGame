using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{

    public float maxSpeed;
    public float damage;

    private Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        body.AddForce(transform.up * maxSpeed);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Attacker")))
        {
            Destroy(this.gameObject);
            collision.gameObject.GetComponent<Hp>().TakeDamage(damage);
        }
        if (!collision.gameObject.layer.Equals(LayerMask.NameToLayer("NonInteractable")))
        {
            Destroy(this.gameObject);
        }
    }
}
