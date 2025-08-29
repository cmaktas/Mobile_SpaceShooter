using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAd : MonoBehaviour
{
    [SerializeField] private string _androidAdUnitId = "Banner_Android";
    [SerializeField] private string _iOSAdUnitId = "Banner_iOS";
    string _adUnitId = null;
    [SerializeField] BannerPosition _bannerPosition = BannerPosition.BOTTOM_CENTER;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif
        LoadBannerAd();
    }

    public void LoadBannerAd()
    {
        if (!Advertisement.isInitialized) return;
        Advertisement.Banner.SetPosition(_bannerPosition);
        BannerLoadOptions options = new()
        {
            loadCallback = OnBannerLoad,
            errorCallback = OnBannerError
        };
        Advertisement.Banner.Load(_adUnitId, options);
    }

    private void OnBannerError(string message)
    {
        Debug.Log("Banner load error");
    }
    
    private void OnBannerLoad()
    {
        Advertisement.Banner.Show(_adUnitId);
    }
}
