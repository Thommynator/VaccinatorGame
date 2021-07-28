using System.Collections;
using UnityEngine;

public class DashAttack : MonoBehaviour
{
    public float dashForce;
    private bool isDashing;
    private float lastDashTime;

    void Start()
    {
        lastDashTime = Time.time - 1000; // subtraction to make sure that there is no cooldown at the start of the game
    }

    public bool IsDashing()
    {
        return isDashing;
    }

    public bool Dash(float dashDuration)
    {
        if (Time.time - lastDashTime <= ShopItems.current.GetValueOf(ItemName.DASH_ATTACK_COOLDOWN))
        {
            return false;
        }

        isDashing = true;
        Rigidbody2D body;
        if (TryGetComponent<Rigidbody2D>(out body))
        {
            body.AddForce(transform.up * dashForce, ForceMode2D.Impulse);
            lastDashTime = Time.time;
            StartCoroutine(DeactivateDash(dashDuration));
        }
        return true;
    }

    private IEnumerator DeactivateDash(float afterDuration)
    {
        yield return new WaitForSeconds(afterDuration);
        isDashing = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Attacker")))
        {
            Attacker attacker;
            if (collision.gameObject.TryGetComponent<Attacker>(out attacker) && isDashing)
            {
                attacker.GetComponent<Hp>().TakeDamage(dashForce);
            }
        }
    }
}
