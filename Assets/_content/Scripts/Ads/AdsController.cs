using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using System;


public static partial class ArgumentKey
{
    public const string GAME_ID = "GAME_ID";
    public const string TEST_ADS = "TEST_ADS";

    public const string BANNER_LOW_PRIORITY = "BANNER_LOW_PRIORITY";
    public const string BANNER_MEDIUM_PRIORITY = "BANNER_MEDIUM_PRIORITY";
    public const string BANNER_HIGH_PRIORITY = "BANNER_HIGH_PRIORITY";

    public const string INTERSTITIAL_LOW_PRIORITY = "INTERSTITIAL_LOW_PRIORITY";
    public const string INTERSTITIAL_MEDIUM_PRIORITY = "INTERSTITIAL_MEDIUM_PRIORITY";
    public const string INTERSTITIAL_HIGH_PRIORITY = "INTERSTITIAL_HIGH_PRIORITY";

    public const string REWARD_LOW_PRIORITY = "REWARD_LOW_PRIORITY";
    public const string REWARD_MEDIUM_PRIORITY = "REWARD_MEDIUM_PRIORITY";
    public const string REWARD_HIGH_PRIORITY = "REWARD_HIGH_PRIORITY";
}

public class AdsController : Controller, IInitialNotification
{
    [SerializeField]
    private AdsDummy adsDummy;
    private AdsDependency adsDependency = default;
    public bool RewardedAdIsReady => adsDependency.RewardedAdIsReady();
    public override void Initialize(Arguments arguments = default)
    {
        if (UnityEngine.Application.platform == RuntimePlatform.Android)
            switch (App.AppModel.AdsModel.AdsService)
            {
                //case AdsService.Admob:
                //    GameObject admobAds = new GameObject("admobAds", typeof(AdmobAdsController));
                //    admobAds.transform.SetParent(this.transform);
                //    adsDependency = admobAds.GetComponent<AdmobAdsController>();
                //    adsDependency.Initialize();
                //    break;
                case AdsService.UnityAds:
                    GameObject unityAds = new GameObject("unityAds", typeof(UnityAdsController));
                    unityAds.transform.SetParent(this.transform);
                    adsDependency = unityAds.GetComponent<UnityAdsController>();
                    adsDependency.Initialize(Arguments.Create()
                        .Put(ArgumentKey.GAME_ID, App.AppModel.AdsModel.UnityAdsGameID)
                        .Put(ArgumentKey.TEST_ADS, App.AppModel.AdsModel.TestAds)
                        .Put(ArgumentKey.BANNER_HIGH_PRIORITY, App.AppModel.AdsModel.UnityAdsBannerID)
                        .Put(ArgumentKey.INTERSTITIAL_HIGH_PRIORITY, App.AppModel.AdsModel.UnityAdsInterstitialID)
                        .Put(ArgumentKey.REWARD_HIGH_PRIORITY, App.AppModel.AdsModel.UnityAdsRewardedAdID));
                    break;
            }
        //else
        //{
        //    //GameObject admobAds = new GameObject("dummyAds", typeof(DummyAdsController));
        //    //admobAds.transform.SetParent(this.transform);
        //    //adsDependency = admobAds.GetComponent<DummyAdsController>();
        //}

        //adsDependency.OnBannerAdLoaded += () => 
        //{ 
        //    if (App.AppModel.SaveModel.NoAds) adsDependency.DestroyBanner(); 
        //};
        //adsDependency.OnRewardedAdClosed += () =>
        //{
        //    App.Notify(EventName.ClosedRewardedAd); 
        
        //};
        //adsDependency.OnUserEarnedReward += () => {
        //    App.Notify(EventName.WatchedRewardedAd); 
        //};

    }
    public void ShowBanner()
    {
        if (App.AppModel.SaveModel.NoAds)
            return;
        adsDependency.ShowBanner();
    }

    public void ShowInterstitial(bool ignoreNoAds = false)
    {
        if (App.AppModel.SaveModel.NoAds && !ignoreNoAds)
            return;
        adsDummy.StartDummy(adsDependency.ShowInterstitial);
    }
    public void ShowRewardedAd()
    {
        adsDummy.StartDummy(adsDependency.ShowRewardedAd);
    }



    public void OnInitialNotification(EventName eventName, Arguments arguments = default)
    {
        switch (eventName)
        {
            case EventName.OnAdsDisabled:
                adsDependency.HideBanner();
                break;
        }
    }

}