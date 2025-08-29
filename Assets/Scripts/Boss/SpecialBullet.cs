using System.Collections;
using UnityEngine;

public class SpecialBullet : MonoBehaviour
{
    [SerializeField] private float damage, speed, rotateSpeed;
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private GameObject enemyBulletPrefab;
    [SerializeField] private Transform[] spawnPoints;

    void Start()
    {
        rb2D.linearVelocity = Vector2.down * speed;
        StartCoroutine(Explode());
    }

    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }

    IEnumerator Explode()
    {
        float randomExplodeTime = Random.Range(1.5f, 2.5f);
        yield return new WaitForSeconds(randomExplodeTime);
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Instantiate(enemyBulletPrefab, spawnPoints[i].position, spawnPoints[i].rotation);
        }
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerStats>().PlayerTakeDamage(damage);
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
