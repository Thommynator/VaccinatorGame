using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DashAttack : MonoBehaviour
{
    public float dashForce;
    public GameObject abilityButton;
    public GameObject cooldownNumber;
    public AudioClip dashSoundClip;
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
        AudioSource.PlayClipAtPoint(dashSoundClip, transform.position);
        StartCoroutine(Cooldown());

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

    private IEnumerator Cooldown()
    {
        float duration = ShopItems.current.GetValueOf(ItemName.DASH_ATTACK_COOLDOWN);
        StartCoroutine(CooldownIndicatorNumber(duration));
        abilityButton.GetComponent<Button>().interactable = false;
        yield return new WaitForSeconds(duration);
        abilityButton.GetComponent<Button>().interactable = true;
    }

    private IEnumerator CooldownIndicatorNumber(float duration)
    {
        int flooredDuration = Mathf.FloorToInt(duration);
        cooldownNumber.GetComponent<TextMeshProUGUI>().text = flooredDuration.ToString();
        float partialSecond = duration % 1.0f;
        yield return new WaitForSeconds(partialSecond);

        for (int i = flooredDuration; i >= 0; i--)
        {
            if (i == 0)
            {
                cooldownNumber.GetComponent<TextMeshProUGUI>().text = "";
            }
            else
            {
                cooldownNumber.GetComponent<TextMeshProUGUI>().text = i.ToString();
            }
            yield return new WaitForSeconds(1);
        }
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
