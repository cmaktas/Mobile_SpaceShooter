using UnityEngine;

public class WinCondition : MonoBehaviour
{
    [SerializeField] private float possibleWinTime;
    [SerializeField] private GameObject[] spawners;
    [SerializeField] private bool hasBoss;

    private float timer;
    public bool canSpawnBoss = false;

    void Update()
    {
        if (EndGameManager.endGameManager.gameOver == true) return;
        timer += Time.deltaTime;
        if (timer >= possibleWinTime)
        {
            if (hasBoss == false)
            {
                EndGameManager.endGameManager.possibleWin = true;
                EndGameManager.endGameManager.StartResolveSequence();
            }
            else
            {
                canSpawnBoss = true;
            }
            
            foreach (GameObject spawner in spawners)
            {
                spawner.SetActive(false);
            }
            
            gameObject.SetActive(false);
        }
    }
}
