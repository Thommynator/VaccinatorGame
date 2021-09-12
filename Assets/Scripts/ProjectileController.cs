using UnityEngine;

public class ProjectileController : MonoBehaviour
{

    public float maxSpeed;
    public AudioClip shootingSoundClip;
    private Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        AudioSource.PlayClipAtPoint(shootingSoundClip, transform.position);
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
        if (!collision.gameObject.layer.Equals(LayerMask.NameToLayer("NonInteractable")))
        {
            Destroy(this.gameObject);
        }
    }
}
