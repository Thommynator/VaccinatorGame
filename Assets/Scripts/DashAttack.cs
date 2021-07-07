using System.Collections;
using UnityEngine;

public class DashAttack : MonoBehaviour
{
    public float dashForce;
    private bool isDashing;

    public bool IsDashing()
    {
        return isDashing;
    }

    public void Dash(float dashDuration)
    {
        isDashing = true;
        Rigidbody2D body;
        if (TryGetComponent<Rigidbody2D>(out body))
        {
            body.AddForce(transform.up * dashForce, ForceMode2D.Impulse);
            StartCoroutine(DeactivateDash(dashDuration));
        }
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
