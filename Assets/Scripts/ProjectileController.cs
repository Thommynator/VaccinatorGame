using System.Collections;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{

    public float maxSpeed;
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
            float damage = ShopItems.current.GetValueOf(ItemName.PROJECTILE_STRENGTH);
            collision.gameObject.GetComponent<Hp>().TakeDamage(damage);
        }
        Destroy(this.gameObject);
    }
}
