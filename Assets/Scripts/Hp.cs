using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp : MonoBehaviour
{

    public AnimationCurve maxHpCurve;
    private float maxHp;
    public delegate void Die();
    public Die dyingMethod;

    private float hp;

    void Start()
    {
        maxHp = maxHpCurve.Evaluate(WaveManager.current.GetCurrentWave());
        SetHp(maxHp);
    }

    public float GetHp()
    {
        return hp;
    }

    public float GetHpPercentage()
    {
        return 100 * hp / maxHp;
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
    }

}
