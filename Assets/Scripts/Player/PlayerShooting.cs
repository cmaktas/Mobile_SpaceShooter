using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private PlayerLaserBullet laserBullet;
    [SerializeField] private float shootingInterval;

    [Header("Basic Canon")]
    [SerializeField] private Transform basicCanon;

    [Header("Upgrade Attack Standart Canons")]
    [SerializeField] private Transform leftCanon;
    [SerializeField] private Transform rightCanon;
    [SerializeField] private Transform secondLeftCanon;
    [SerializeField] private Transform secondRightCanon;

    [Header("Upgrade Attack Rotated Canons")]
    [SerializeField] private Transform leftRotatedCanon;
    [SerializeField] private Transform rightRotatedCanon;
    [SerializeField] private Transform secondLeftRotatedCanon;
    [SerializeField] private Transform secondRightRotatedCanon;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;

    private int upgradeCanonLevel;
    private float intervalReset;
    private ObjectPool<PlayerLaserBullet> laserBulletPool;

    private static readonly int[] StandardCountPerLevel = { 1, 3, 5, 5, 5 };
    private static readonly int[] RotatedCountPerLevel  = { 0, 0, 0, 2, 4 };

    private Transform[] standardOrder;
    private Transform[] rotatedOrder;

    void Awake()
    {
        laserBulletPool = new ObjectPool<PlayerLaserBullet>(CreateLaserBullet, TakeBulletFromPool,
                                                            ReturnBulletFromPool, DestroyBulletFromPool,
                                                            true, 10, 30);
        standardOrder = new Transform[] { basicCanon, leftCanon, rightCanon, secondLeftCanon, secondRightCanon };
        rotatedOrder  = new Transform[] { leftRotatedCanon, rightRotatedCanon, secondLeftRotatedCanon, secondRightRotatedCanon };
    }

    void Start()
    {
        intervalReset = shootingInterval;
    }

    void Update()
    {
        shootingInterval -= Time.deltaTime;
        if (shootingInterval <= 0)
        {
            Shoot();
            shootingInterval = intervalReset;
        }
    }

    public void IncreaseCanonLevel(int increaseAmount)
    {
        upgradeCanonLevel += increaseAmount;
        if (upgradeCanonLevel > 4)
        {
            upgradeCanonLevel = 4;
        }
    }

    public void DecreaseCanonLevel()
    {
        upgradeCanonLevel -= 1;
        if (upgradeCanonLevel < 0)
        {
            upgradeCanonLevel = 0;
        }
    }

    private void Shoot()
    {
        audioSource.Play();

        int standardToFire = StandardCountPerLevel[upgradeCanonLevel];
        int rotatedToFire  = RotatedCountPerLevel[upgradeCanonLevel];

        FireStandard(standardToFire);
        FireRotated(rotatedToFire);
    }

    private void FireStandard(int count)
    {
        for (int i = 0; i < count && i < standardOrder.Length; i++)
        {
            if (standardOrder[i] != null)
            {
                InstantiateBullet(standardOrder[i], false);
            }
        }
    }

    private void FireRotated(int count)
    {
        for (int i = 0; i < count && i < rotatedOrder.Length; i++)
        {
            if (rotatedOrder[i] != null)
            {
                InstantiateBullet(rotatedOrder[i], true);
            }
        }
    }

    private void InstantiateBullet(Transform canon, bool isRotated)
    {
        PlayerLaserBullet playerLaserBullet = laserBulletPool.Get();
        playerLaserBullet.transform.position = canon.position;
        if (isRotated)
        {
            playerLaserBullet.transform.rotation = canon.rotation;
            playerLaserBullet.SetDirectionAndSpeed();
        }
    }
    
    private PlayerLaserBullet CreateLaserBullet()
    {
        PlayerLaserBullet bullet = Instantiate(laserBullet, transform.position, transform.rotation);
        bullet.SetPool(laserBulletPool);
        return bullet;
    }

    private void TakeBulletFromPool(PlayerLaserBullet laserBullet)
    {
        laserBullet.gameObject.SetActive(true);
    }

    private void ReturnBulletFromPool(PlayerLaserBullet laserBullet)
    {
        laserBullet.gameObject.SetActive(false);
    }

    private void DestroyBulletFromPool(PlayerLaserBullet laserBullet)
    {
        Destroy(laserBullet.gameObject);
    }
}
