using UnityEngine;

public class EnemyShip : Enemy
{
    [SerializeField] private float speed;
    [SerializeField] private float shootInterval;
    [SerializeField] private Transform leftCanon;
    [SerializeField] private Transform rightCanon;
    [SerializeField] private GameObject bulletPrefab;

    private float shootTimer;

    void Start()
    {
        rb2D.linearVelocity = Vector2.down * speed;
    }

    void Update()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootInterval)
        {
            Instantiate(bulletPrefab, leftCanon.position, Quaternion.identity);
            Instantiate(bulletPrefab, rightCanon.position, Quaternion.identity);
            shootTimer = 0;
        }
    }

    public override void HurtSequence()
    {
        base.HurtSequence();
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("dmg")) return;
        animator.SetTrigger("damage");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerStats>().PlayerTakeDamage(damage);
            DeathSequence();
        }
    }

}
