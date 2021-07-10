using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp : MonoBehaviour
{

    public float maxHp;
    public delegate void Die();
    public Die dyingMethod;

    private float hp;

    // Start is called before the first frame update
    void Start()
    {
        SetHp(maxHp);

    }

    public float GetHp()
    {
        return hp;
    }
    public void IncreaseHp(float hpIncrease)
    {
        SetHp(hp + hpIncrease);
    }

    public void TakeDamage(float damage)
    {
        SetHp(hp - damage);
        if (hp <= 0)
        {
            if (dyingMethod != null)
            {
                dyingMethod();
            }
        }
    }

    private void SetHp(float newHp)
    {
        hp = Mathf.Clamp(newHp, 0, maxHp);
        // hpText.text = hp.ToString();
    }
}