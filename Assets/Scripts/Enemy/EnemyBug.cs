using UnityEngine;

public class EnemyBug : Enemy
{
    [SerializeField] private float speed;

    void Start()
    {
        rb2D.linearVelocity = Vector2.down * speed;
    }

    public override void HurtSequence()
    {
        base.HurtSequence();
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("dmg")) return;
        animator.SetTrigger("damage");
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlayerStats playerStats = collider.GetComponent<PlayerStats>();
            playerStats.PlayerTakeDamage(damage);
            DeathSequence();
        }
    } 

}
