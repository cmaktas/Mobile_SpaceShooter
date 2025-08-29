using UnityEngine;

[CreateAssetMenu(fileName = "PowerUpSpawner", menuName = "Scriptable Objects/PowerUpSpawner")]
public class PowerUpSpawner : ScriptableObject
{
    public GameObject[] powerUps;
    public int spanwThreashold;

    public void SpawnPowerUp(Vector3 spawnPos)
    {
        if (Random.Range(0, 100) > spanwThreashold)
        {
            Instantiate(powerUps[Random.Range(0, powerUps.Length)], spawnPos, Quaternion.identity);
        }    
    }
}
