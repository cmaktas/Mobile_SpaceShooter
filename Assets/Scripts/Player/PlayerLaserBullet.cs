using UnityEngine;
using UnityEngine.Pool;

public class PlayerLaserBullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private Rigidbody2D rb2D;

    private ObjectPool<PlayerLaserBullet> laserBulletPool;

    void OnEnable()
    {
        SetDirectionAndSpeed();
    }

    void OnDisable()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        enemy.TakeDamage(damage);
        ReleaseBulletFromPool();
    }

    public void SetPool(ObjectPool<PlayerLaserBullet> laserBulletPool)
    {
        this.laserBulletPool = laserBulletPool;
    }

    private void ReleaseBulletFromPool()
    {
        if (!gameObject.activeSelf) return;
        laserBulletPool.Release(this);
    }

    public void SetDirectionAndSpeed()
    {
        rb2D.linearVelocity = transform.up * speed;
    }

    void OnBecameInvisible()
    {
        ReleaseBulletFromPool();
    }

}
