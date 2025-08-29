using UnityEngine;

public class BossStats : Enemy
{
    [SerializeField] BossController bossController;

    public override void HurtSequence()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("dmg")) return;
        animator.SetTrigger("damage");
    }

    public override void DeathSequence()
    {
        bossController.ChangeState(BossState.Death);
        base.DeathSequence();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerStats>().PlayerTakeDamage(damage);
        }
    }

}
