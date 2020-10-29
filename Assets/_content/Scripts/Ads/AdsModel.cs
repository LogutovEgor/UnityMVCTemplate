using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AdsService { Admob, UnityAds }
public class AdsModel : Model
{
    [SerializeField]
    private bool testAds = default;
    public bool TestAds => testAds;

    [SerializeField]
    private AdsService adsService = default;
    public AdsService AdsService => adsService;

    private const string ADMOB_BANNER_TEST_ID = "ca-app-pub-3940256099942544/6300978111";
    private const string ADMOB_INTERSTITIAL_TEST_ID = "ca-app-pub-3940256099942544/1033173712";
    private const string ADMOB_REWARDED_VIDEO_TEST_ID = "ca-app-pub-3940256099942544/5224354917";

    #region Banners

    [SerializeField]
    private string admobBannerLowPriorityID = default;
    public string AdmobBannerLowPriorityID => !testAds ? admobBannerLowPriorityID : ADMOB_BANNER_TEST_ID;

    [SerializeField]
    private string admobBannerMediumPriorityID = default;
    public string AdmobBannerMediumPriorityID => !testAds ? admobBannerMediumPriorityID : ADMOB_BANNER_TEST_ID;

    [SerializeField]
    private string admobBannerHighPriorityID = default;
    public string AdmobBannerHighPriorityID => !testAds ? admobBannerHighPriorityID : ADMOB_BANNER_TEST_ID;

    #endregion

    #region Interstitials
    [SerializeField]
    private string admobInterstitialLowPriorityID = default;
    public string AdmobInterstitialLowPriorityID => !testAds ? admobInterstitialLowPriorityID : ADMOB_INTERSTITIAL_TEST_ID;

    [SerializeField]
    private string admobInterstitialMediumPriorityID = default;
    public string AdmobInterstitialMediumPriorityID => !testAds ? admobInterstitialMediumPriorityID : ADMOB_INTERSTITIAL_TEST_ID;

    [SerializeField]
    private string admobInterstitialHighPriorityID = default;
    public string AdmobInterstitialHighPriorityID => !testAds ? admobInterstitialHighPriorityID : ADMOB_INTERSTITIAL_TEST_ID;

    #endregion

    #region RewardedAds

    [SerializeField]
    private string admobRewardedAdLowPriorityID = default;
    public string AdmobRewardedAdLowPriorityID => !testAds ? admobRewardedAdLowPriorityID : ADMOB_REWARDED_VIDEO_TEST_ID;

    [SerializeField]
    private string admobRewardedAdMediumPriorityID = default;
    public string AdmobRewardedAdMediumPriorityID => !testAds ? admobRewardedAdMediumPriorityID : ADMOB_REWARDED_VIDEO_TEST_ID;

    [SerializeField]
    private string admobRewardedAdHighPriorityID = default;
    public string AdmobRewardedAdHighPriorityID => !testAds ? admobRewardedAdHighPriorityID : ADMOB_REWARDED_VIDEO_TEST_ID;

    #endregion

    [SerializeField]
    private float refreshBannerTime;
    public float RefreshBannerTime => refreshBannerTime;

    [SerializeField]
    private string unityAdsGameID = default;
    public string UnityAdsGameID => unityAdsGameID;

    [SerializeField]
    private string unityAdsBannerID = default;
    public string UnityAdsBannerID => unityAdsBannerID;

    [SerializeField]
    private string unityAdsInterstitialID = default;
    public string UnityAdsInterstitialID => unityAdsInterstitialID;

    [SerializeField]
    private string unityAdsRewardedAdID = default;
    public string UnityAdsRewardedAdID => unityAdsRewardedAdID;


    public override void Initialize(Arguments arguments)
    {
    }
}
