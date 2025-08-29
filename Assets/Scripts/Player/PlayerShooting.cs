using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject laserBullet;
    [SerializeField] private float shootingInterval;

    [Header("Basic Canon")]
    [SerializeField] private Transform basicCanonPoint;

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

    [SerializeField] private AudioSource audioSource;

    private int upgradeCanonLevel;
    private float intervalReset;

    void Start()
    {
        intervalReset = shootingInterval;
    }

    // Update is called once per frame
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
        switch (upgradeCanonLevel)
        {
            case 0:
                Instantiate(laserBullet, basicCanonPoint.position, Quaternion.identity);
                break;
            case 1:
                Instantiate(laserBullet, basicCanonPoint.position, Quaternion.identity);
                Instantiate(laserBullet, leftCanon.position, Quaternion.identity);
                Instantiate(laserBullet, rightCanon.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(laserBullet, basicCanonPoint.position, Quaternion.identity);
                Instantiate(laserBullet, leftCanon.position, Quaternion.identity);
                Instantiate(laserBullet, rightCanon.position, Quaternion.identity);
                Instantiate(laserBullet, secondLeftCanon.position, Quaternion.identity);
                Instantiate(laserBullet, secondRightCanon.position, Quaternion.identity);
                break;
            case 3:
                Instantiate(laserBullet, basicCanonPoint.position, Quaternion.identity);
                Instantiate(laserBullet, leftCanon.position, Quaternion.identity);
                Instantiate(laserBullet, rightCanon.position, Quaternion.identity);
                Instantiate(laserBullet, secondLeftCanon.position, Quaternion.identity);
                Instantiate(laserBullet, secondRightCanon.position, Quaternion.identity);
                Instantiate(laserBullet, leftRotatedCanon.position, leftRotatedCanon.rotation);
                Instantiate(laserBullet, rightRotatedCanon.position, rightRotatedCanon.rotation);
                break;
            case 4:
                Instantiate(laserBullet, basicCanonPoint.position, Quaternion.identity);
                Instantiate(laserBullet, leftCanon.position, Quaternion.identity);
                Instantiate(laserBullet, rightCanon.position, Quaternion.identity);
                Instantiate(laserBullet, secondLeftCanon.position, Quaternion.identity);
                Instantiate(laserBullet, secondRightCanon.position, Quaternion.identity);
                Instantiate(laserBullet, leftRotatedCanon.position, leftRotatedCanon.rotation);
                Instantiate(laserBullet, rightRotatedCanon.position, rightRotatedCanon.rotation);
                Instantiate(laserBullet, secondLeftRotatedCanon.position, secondLeftRotatedCanon.rotation);
                Instantiate(laserBullet, secondRightRotatedCanon.position, secondRightRotatedCanon.rotation);
                break;
        }
    }
}
