using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public void LoadLevelString(string levelName)
    {
        ScreenFadeController.screenFader.FaderLoadLevelName(levelName);
    }

    public void LoadLevelInt(int levelIndex)
    {
        ScreenFadeController.screenFader.FaderLoadLevelIndex(levelIndex);
    }

    public void RestartLevel()
    {
        ScreenFadeController.screenFader.FaderLoadLevelIndex(SceneManager.GetActiveScene().buildIndex);
    }
}
