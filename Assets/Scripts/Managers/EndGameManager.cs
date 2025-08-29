using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    public static EndGameManager endGameManager;
    public bool gameOver;
    public bool possibleWin;
    private PanelController panelController;
    private TextMeshProUGUI scoreText;
    public int score;
    private PlayerStats playerStats;
    private RewardedAd rewardedAd;

    [HideInInspector]
    public string levelUnlock = "LevelUnlock";

    void Awake()
    {
        if (endGameManager is null)
        {
            endGameManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void UpdateScore(int addScore)
    {
        score += addScore;
        scoreText.text = "Score: " + score.ToString();
    }

    public void StartResolveSequence()
    {
        StopCoroutine(nameof(ResolveSequence));
        StartCoroutine(ResolveSequence());
    }

    private IEnumerator ResolveSequence()
    {
        yield return new WaitForSeconds(2);
        ResolveGame();
    }

    public void ResolveGame()
    {
        if (!gameOver && possibleWin)
        {
            WinGame();
        }
        else if (!possibleWin && gameOver)
        {
            LooseGameAd();
        }
        else if (possibleWin && gameOver)
        {
            LooseGame();
        }
    }

    public void WinGame()
    {
        playerStats.canTakeDamage = false;
        ScoreSet();
        panelController.ActiveWin();
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextLevel > PlayerPrefs.GetInt(levelUnlock, 0))
        {
            PlayerPrefs.SetInt(levelUnlock, nextLevel);
        }

    }

    public void LooseGame()
    {
        ScoreSet();
        panelController.ActivateLoose();
    }

    public void LooseGameAd()
    {
        ScoreSet();
        if (rewardedAd.adNumber > 0)
        {
            rewardedAd.adNumber -= 1;
            panelController.ActivateAdLoose();
        }
        else
        {
            panelController.ActivateLoose();
        }
        
    }

    private void ScoreSet()
    {
        PlayerPrefs.SetInt("Score" + SceneManager.GetActiveScene().name, score);
        int highScore = PlayerPrefs.GetInt("HighScore" + SceneManager.GetActiveScene().name, 0);
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore" + SceneManager.GetActiveScene().name, score);
        }
        score = 0;
    }

    public void RegisterPanelController(PanelController panelController)
    {
        this.panelController = panelController;
    }

    public void RegisterScoreText(TextMeshProUGUI scoreText)
    {
        this.scoreText = scoreText;
    }

    public void RegisterPlayerStats(PlayerStats playerStats)
    {
        this.playerStats = playerStats;
    }

    public void RegisterRewardedAd(RewardedAd rewardedAd)
    {
        this.rewardedAd = rewardedAd;
    }
}
