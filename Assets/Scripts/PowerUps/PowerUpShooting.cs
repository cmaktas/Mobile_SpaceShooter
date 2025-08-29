using UnityEngine;

public class PowerUpShooting : MonoBehaviour
{
    [SerializeField] private AudioClip clipToPlay;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerShooting playerShooting = collision.GetComponent<PlayerShooting>();
            playerShooting.IncreaseCanonLevel(Random.Range(1,5));
            AudioSource.PlayClipAtPoint(clipToPlay, transform.position, 1f);
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
