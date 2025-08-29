using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _androidAdUnitId = "Interstitial_Android";
    [SerializeField] string _iOSAdUnitId = "Interstitial_iOS";
    [SerializeField] BannerAd bannerAd;
    [SerializeField] private int timeToSkip = 1;
    string _adUnitId;

    void Awake()
    {
        // Get the Ad Unit ID for the current platform:
        /* _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
        ? _iOSAdUnitId
        : _androidAdUnitId; */

#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
            _adUnitId = _androidAdUnitId;
#endif

        int skipNumber = PlayerPrefs.GetInt("interstitial ", timeToSkip);
        if (skipNumber != 0)
        {
            skipNumber -= 1;
            PlayerPrefs.SetInt("interstitial ", skipNumber);
        }
        else
        {
            LoadAd();
            PlayerPrefs.SetInt("interstitial ", timeToSkip);
        }
        
        
    }

    // Load content to the Ad Unit:
    public void LoadAd()
    {
        if (!Advertisement.isInitialized) return;

        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    // Show the loaded content in the Ad Unit:
    public void ShowAd()
    {
        // Note that if the ad content wasn't previously loaded, this method will fail
        Debug.Log("Showing Ad: " + _adUnitId);
        Advertisement.Show(_adUnitId, this);
    }

    // Implement Load Listener and Show Listener interface methods: 
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        // Optionally execute code if the Ad Unit successfully loads content.
        ShowAd();
    }
    
    public void OnUnityAdsFailedToLoad(string _adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {_adUnitId} - {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
    }
    
    public void OnUnityAdsShowFailure(string _adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {_adUnitId}: {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to show, such as loading another ad.
    }

    public void OnUnityAdsShowStart(string _adUnitId)
    {
        Debug.Log("Ads show has started.");
        Advertisement.Banner.Hide();
        Time.timeScale = 0;
    }
     
    public void OnUnityAdsShowClick(string _adUnitId)
    {
        Debug.Log("Ads show is clicked.");
    }

    public void OnUnityAdsShowComplete(string _adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("Ads show is completed.");
        bannerAd.LoadBannerAd();
        Time.timeScale = 1;
    }

   
}
