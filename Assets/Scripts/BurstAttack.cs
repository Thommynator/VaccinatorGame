using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BurstAttack : MonoBehaviour
{

    public int numberOfBurstShotProjectiles;
    public GameObject abilityButton;
    public GameObject cooldownNumber;
    private float lastBurstAttackTime;


    void Start()
    {
        lastBurstAttackTime = Time.time - 1000; // subtraction to make sure that there is no cooldown at the start of the game
    }

    public bool Fire(Vector3 initialVelocity)
    {
        if (Time.time - lastBurstAttackTime <= ShopItems.current.GetValueOf(ItemName.BURST_ATTACK_COOLDOWN))
        {
            return false;
        }

        StartCoroutine(Cooldown());
        lastBurstAttackTime = Time.time;
        StartCoroutine(SpawnProjectiles(numberOfBurstShotProjectiles, initialVelocity));
        return true;
    }

    private IEnumerator SpawnProjectiles(int projectiles, Vector3 initialVelocity)
    {
        ProjectileSpawner projectileSpawner = GetComponent<ProjectileSpawner>();
        for (int i = 0; i < projectiles; i++)
        {
            projectileSpawner.Fire(transform.position, transform.position + transform.up, initialVelocity);
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator Cooldown()
    {
        float duration = ShopItems.current.GetValueOf(ItemName.BURST_ATTACK_COOLDOWN);
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
}
