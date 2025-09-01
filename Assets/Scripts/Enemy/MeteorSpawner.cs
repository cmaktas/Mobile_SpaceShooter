using System.Collections;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] meteors;
    [SerializeField] private float spawnTime;

    private float timer = 0f;
    private Camera mainCamera;
    private float maxLeft, maxRight, projectionZ, yPos;

    void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(SetBoundaries());
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > spawnTime)
        {
            InstantiateMeteor();
            timer = 0;
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

    private void InstantiateMeteor()
    {
        GameObject meteor = Instantiate(meteors[Random.Range(0, meteors.Length)],
                        new Vector3(Random.Range(maxLeft, maxRight), yPos, -5),
                        Quaternion.Euler(0, 0, Random.Range(0, 360)));
        meteor.transform.localScale = new Vector3(Random.Range(0.9f, 1.1f), Random.Range(0.9f, 1.1f), 1);
    }

    private void OnBecameInvisible() 
    {
        Destroy(gameObject);
    }
}
