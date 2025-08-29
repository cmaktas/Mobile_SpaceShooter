using UnityEngine;

public class PowerUpHeal : MonoBehaviour
{
    [SerializeField] private int healAmount;
    [SerializeField] private AudioClip clipToPlay;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerStats playerStats = collision.GetComponent<PlayerStats>();
            playerStats.AddHealth(healAmount);
            AudioSource.PlayClipAtPoint(clipToPlay, transform.position, 1f);
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
