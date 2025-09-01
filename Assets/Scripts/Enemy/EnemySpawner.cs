using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [Header("Enemy Prefabs")]
    [SerializeField] private GameObject[] enemies;

    [Space(15)]
    [SerializeField] private float enemySpawnTime;

    [Header("Boss")]
    [SerializeField] private GameObject bossPrefab;

    [SerializeField] private WinCondition winCondition;

    private float enemyTimer;
    private Camera mainCamera;
    private float maxLeft, maxRight, projectionZ, yPos;

    void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(SetBoundaries());
    }

    void Update()
    {
        EnemySpawn();
    }

    private void EnemySpawn()
    {
        enemyTimer += Time.deltaTime;
        if (enemyTimer >= enemySpawnTime)
        {
            Instantiate(enemies[Random.Range(0, enemies.Length)],
                        new Vector3(Random.Range(maxLeft, maxRight), yPos, 0),
                        Quaternion.identity);
            enemyTimer = 0;
        }
    }

    private IEnumerator SetBoundaries()
    {
        yield return new WaitForSeconds(0.4f);
        projectionZ = Mathf.Abs(mainCamera.transform.position.z - transform.position.z);
        maxLeft = mainCamera.ViewportToWorldPoint(new Vector3(0.15f, 0f, projectionZ)).x;
        maxRight = mainCamera.ViewportToWorldPoint(new Vector3(0.85f, 0f, projectionZ)).x;
        yPos = mainCamera.ViewportToWorldPoint(new Vector2(0, 1.1f)).y;

    }

    void OnDisable()
    {
        if (bossPrefab != null && winCondition.canSpawnBoss)
        {
            Vector2 spawnPos = mainCamera.ViewportToWorldPoint(new Vector2(0.5f, 1.2f));
            Instantiate(bossPrefab, spawnPos, Quaternion.identity);
        }
    }
}
