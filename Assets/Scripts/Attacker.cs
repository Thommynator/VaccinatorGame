using UnityEngine;

public class Attacker : MonoBehaviour
{
    public float damage;
    public float attackCooldown;
    public GameObject explosionEffectPrefab;
    public GameObject bonusRewardPrefab;
    private float lastAttackTime;
    private bool isAttacking;


    void Start()
    {
        GameEvents.current.onAttackerAttachsToCell += StopMoving;
        GameEvents.current.onAttackerDetachesFromCell += StartMoving;

        GetComponent<Hp>().dyingMethod = Die;
        lastAttackTime = Time.timeSinceLevelLoad;
        isAttacking = false;

    }

    public void AttackCell(GameObject cell)
    {
        LookAt(cell);
        if (Time.timeSinceLevelLoad - lastAttackTime > attackCooldown)
        {
            cell.GetComponent<Hp>().TakeDamage(damage);
            lastAttackTime = Time.timeSinceLevelLoad;
        }
    }

    public void Die()
    {
        GameEvents.current.IncreaseVisibleArea(ShopItems.current.GetValueOf(ItemName.VISIBLE_AREA_GAIN));
        GameEvents.current.ShakeCamera(0.25f, 5);
        GameObject explosionFx = GameObject.Instantiate<GameObject>(explosionEffectPrefab, transform.position, Quaternion.identity);
        Destroy(explosionFx, 3);

        // reward bonus when attacker is attacking
        if (isAttacking)
        {
            GameObject.Instantiate(bonusRewardPrefab, this.transform.position, Quaternion.identity);
            float bonusFactor = ShopItems.current.GetValueOf(ItemName.REWARD_BONUS);
            GetComponent<Reward>().rewardAmount *= ((int)Mathf.Round(bonusFactor));
        }

        Destroy(this.gameObject);
    }

    private void StopMoving(GameObject attacker, GameObject cell)
    {
        if (attacker == this.gameObject)
        {
            isAttacking = true;
            GetComponent<MovementBehavior>().enabled = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            LookAt(cell);
        }
    }

    private void StartMoving(GameObject attacker, GameObject cell)
    {
        if (attacker == this.gameObject)
        {
            isAttacking = false;
            GetComponent<MovementBehavior>().enabled = true;
        }
    }

    private void LookAt(GameObject target)
    {
        transform.up = target.transform.position - transform.position;
    }

    private void OnDestroy()
    {
        if (this.gameObject.TryGetComponent<FixedJoint2D>(out FixedJoint2D joint))
        {
            // detach from cell if it's attached to it
            GameObject linkedCell = joint?.connectedBody?.gameObject;
            if (linkedCell != null)
            {
                GameEvents.current.AttackerDetachesFromCell(this.gameObject, joint.connectedBody?.gameObject);
            }
        }
        GameEvents.current.AttackerDies(this.gameObject);
        GameEvents.current.onAttackerAttachsToCell -= StopMoving;
        GameEvents.current.onAttackerDetachesFromCell -= StartMoving;
    }
}
