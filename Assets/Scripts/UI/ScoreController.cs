using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TextMeshProUGUI textMeshProToRegiester = GetComponent<TextMeshProUGUI>();
        EndGameManager.endGameManager.RegisterScoreText(textMeshProToRegiester);
        textMeshProToRegiester.text = "Score: 0";
    }

}
