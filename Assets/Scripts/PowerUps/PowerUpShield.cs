using UnityEngine;

public class PowerUpShield : MonoBehaviour
{
    [SerializeField] private AudioClip clipToPlay;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ShieldActivator shieldActivator = collision.GetComponent<ShieldActivator>();
            shieldActivator.ActivateShield();
            AudioSource.PlayClipAtPoint(clipToPlay, transform.position, 1f);
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
