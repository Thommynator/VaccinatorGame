using System.Collections;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{

    public float maxSpeed;
    private Rigidbody2D body;

    private int layer;
    private int temporayNonCollisionLayer;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        layer = this.gameObject.layer;
        temporayNonCollisionLayer = LayerMask.NameToLayer("NonInteractable");

        StartCoroutine(ToggleCollisionAtStart());
    }

    void FixedUpdate()
    {
        body.AddForce(transform.up * maxSpeed);
    }

    IEnumerator ToggleCollisionAtStart()
    {
        this.gameObject.layer = temporayNonCollisionLayer;
        yield return new WaitForSeconds(1);
        this.gameObject.layer = layer;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Attacker")))
        {
            Destroy(this.gameObject);
            float damage = ShopItems.current.GetValueOf(ItemName.PROJECTILE_STRENGTH);
            collision.gameObject.GetComponent<Hp>().TakeDamage(damage);
        }
        if (!collision.gameObject.layer.Equals(LayerMask.NameToLayer("NonInteractable"))
            && !collision.gameObject.layer.Equals(this.gameObject.layer))
        {
            Destroy(this.gameObject);
        }
    }
}
