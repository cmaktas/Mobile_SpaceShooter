using UnityEngine;

public class PanelController : MonoBehaviour
{

    [SerializeField] private CanvasGroup cGroup;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject looseScreen;
    [SerializeField] private GameObject adLooseScreen;

    void Start()
    {
        EndGameManager.endGameManager.RegisterPanelController(this);
    }

    public void ActiveWin()
    {
        cGroup.alpha = 1;
        winScreen.SetActive(true);
    }

    public void ActivateLoose()
    {
        cGroup.alpha = 1;
        looseScreen.SetActive(true);
    }

    public void ActivateAdLoose()
    {
        cGroup.alpha = 1;
        adLooseScreen.SetActive(true);
    }

    public void DeactivateAdLoose()
    {
        cGroup.alpha = 0;
        adLooseScreen.SetActive(false);
    }


}
