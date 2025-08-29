using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenFadeController : MonoBehaviour
{
    public static ScreenFadeController screenFader;

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Image loadingBar;
    [SerializeField] private float changeValue, waitTime;
    [SerializeField] private bool fadeStarted = false;

    void Awake()
    {
        if (screenFader is null)
        {
            screenFader = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FaderLoadLevelName(string levelName)
    {
        StartCoroutine(FadeOutLevelName(levelName));
    }

    public void FaderLoadLevelIndex(int levelIndex)
    {
        StartCoroutine(FadeOutLevelIndex(levelIndex));
    }

    IEnumerator FadeIn()
    {
        loadingScreen.SetActive(false);
        fadeStarted = false;
        while (canvasGroup.alpha > 0)
        {
            if (fadeStarted) yield break;
            canvasGroup.alpha -= changeValue;
            yield return new WaitForSeconds(waitTime);
        }
    }

    IEnumerator FadeOutLevelName(string levelName)
    {
        if (fadeStarted) yield break;
        fadeStarted = true;
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += changeValue;
            yield return new WaitForSeconds(waitTime);
        }
        
        // SceneManager.LoadScene(levelName);
        // yield return new WaitForSeconds(0.1f);
        yield return StartCoroutine(AsyncSceneLoad(levelName: levelName, levelIndex: null));
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeOutLevelIndex(int levelIndex)
    {
        if (fadeStarted) yield break;
        fadeStarted = true;
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += changeValue;
            yield return new WaitForSeconds(waitTime);
        }

        yield return StartCoroutine(AsyncSceneLoad(levelName: null, levelIndex: levelIndex));

        // SceneManager.LoadScene(levelIndex);
        // yield return new WaitForSeconds(0.1f);
        StartCoroutine(FadeIn());
    }

    private IEnumerator AsyncSceneLoad (string levelName, int? levelIndex = null)
    {
        AsyncOperation ao = !string.IsNullOrEmpty(levelName)
        ? SceneManager.LoadSceneAsync(levelName)
        : SceneManager.LoadSceneAsync(levelIndex.Value);
        ao.allowSceneActivation = false;
        loadingScreen.SetActive(true);
        loadingBar.fillAmount = 0;
        while (ao.isDone == false)
        {
            loadingBar.fillAmount = ao.progress / 0.9f;
            if (ao.progress == 0.9f) {
                ao.allowSceneActivation = true;
            }
            yield return null;
        }
    }

}
