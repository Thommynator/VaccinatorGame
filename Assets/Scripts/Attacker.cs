using UnityEngine;

public class Attacker : MonoBehaviour
{
    public float damage;
    public float attackCooldown;
    public AudioClip explosionSound;
    public GameObject explosionEffectPrefab;
    private float lastAttackTime;


    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onAttackerAttachsToCell += StopMoving;
        GameEvents.current.onAttackerDetachesFromCell += StartMoving;

        GetComponent<Hp>().dyingMethod = Die;
        lastAttackTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameEvents.current.AttackerDetachesFromCell(this.gameObject, GameObject.Find("Cell"));
        }
    }

    public void AttackCell(GameObject cell)
    {
        LookAt(cell);
        if (Time.time - lastAttackTime > attackCooldown)
        {
            cell.GetComponent<Hp>().TakeDamage(damage);
            lastAttackTime = Time.time;
        }
    }

    public void Die()
    {
        GameEvents.current.IncreaseVisibleArea(1.0f);
        GameEvents.current.ShakeCamera(0.25f);
        GameObject explosionFx = GameObject.Instantiate<GameObject>(explosionEffectPrefab, transform.position, Quaternion.identity);
        Destroy(explosionFx, 3);
        AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        Destroy(this.gameObject);
    }

    private void StopMoving(GameObject attacker, GameObject cell)
    {
        if (attacker == this.gameObject)
        {
            GetComponent<RandomWalker>().enabled = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            LookAt(cell);
        }
    }

    private void StartMoving(GameObject attacker, GameObject cell)
    {
        if (attacker == this.gameObject)
        {
            GetComponent<RandomWalker>().enabled = true;
        }
    }

    private void LookAt(GameObject target)
    {
        transform.up = target.transform.position - transform.position;
    }

    private void OnDestroy()
    {
        GameEvents.current.AttackerDies(this.gameObject);
        GameEvents.current.onAttackerAttachsToCell -= StopMoving;
        GameEvents.current.onAttackerDetachesFromCell -= StartMoving;
    }
}
