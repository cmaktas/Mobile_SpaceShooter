using UnityEngine;

public class WinCondition : MonoBehaviour
{
    [SerializeField] private float possibleWinTime;
    [SerializeField] private GameObject[] spawners;
    [SerializeField] private bool hasBoss;

    private float timer;
    public bool canSpanwBoss = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
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
                canSpanwBoss = true;
            }
            
            foreach (GameObject spawner in spawners)
            {
                spawner.SetActive(false);
            }
            
            gameObject.SetActive(false);
        }
    }
}
