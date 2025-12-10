using UnityEngine;
using UnityEngine.Advertisements;

public class AdService : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private bool _testMode;

#if UNITY_ANDROID
    private const string _gameId = "6002045";
    private const string _interstitialId = "Interstitial_Android";
    private const string _rewardedId = "Rewarded_Android";
    private const string _bannerAndroid = "Banner_Android";
#endif


    private void Awake()
    {
        Advertisement.Initialize(_gameId, _testMode, this);
    }

    #region Load
    public void OnInitializationComplete()
    {
        Debug.Log("Init complete");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log("Init failed");
    }
    #endregion

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Load complete");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log("Load failed");
    }

    
    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
    }

    public void OnUnityAdsShowStart(string placementId)
    {
    }

    public void OnUnityAdsShowClick(string placementId)
    {
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if(showCompletionState == UnityAdsShowCompletionState.COMPLETED)
        {
            GetReward();
        }
    }

    #region Rewarded
    [ContextMenu("Load Rewarded")]
    public void LoadRewarded()
    {
        Advertisement.Load(_rewardedId, this);
    }

    [ContextMenu("Show Rewarded")]
    public void ShowRewarded()
    {
        Advertisement.Show(_rewardedId, this);
    }

    private void GetReward()
    {
        Debug.Log("Get Reward");
    }
    #endregion

    #region Banner
    [ContextMenu("Show Banner")]
    public void ShowBanner()
    {
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        BannerLoadOptions loadOptions = new BannerLoadOptions()
        {
            loadCallback = () =>
            {
                BannerOptions bannerOptions = new BannerOptions()
                {
                    clickCallback = () => Debug.Log("Click banner"),
                    showCallback = () => Debug.Log("Show banner"),
                    hideCallback = () => Debug.Log("Hide banner")
                };

                Advertisement.Banner.Show(_bannerAndroid, bannerOptions);
            },
            errorCallback = msg => Debug.LogError("Banner loading failed")
        };

        Advertisement.Banner.Load(_bannerAndroid, loadOptions);
    }

    [ContextMenu("Hide Banner")]
    public void HideBunner()
    {
        Advertisement.Banner.Hide();
    }
    #endregion
}
