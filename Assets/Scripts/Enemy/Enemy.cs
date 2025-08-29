using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float health;
    [SerializeField] protected Rigidbody2D rb2D;
    [SerializeField] GameObject explosionVFXPrefab;
    [SerializeField] protected float damage;
    [SerializeField] protected Animator animator;
    [Header("Score") ,SerializeField] protected int scoreValue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        HurtSequence();

        if (health <= 0)
        {
            DeathSequence();
        }
    }

    public virtual void HurtSequence()
    {

    }

    public virtual void DeathSequence()
    {
        PlayDeathAnimation();
        EndGameManager.endGameManager.UpdateScore(scoreValue);
        Destroy(gameObject);
    }

    public void PlayDeathAnimation()
    {
        Instantiate(explosionVFXPrefab, transform.position, transform.rotation);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
