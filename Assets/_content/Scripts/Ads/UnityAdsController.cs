using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdsController : AdsDependency, IUnityAdsListener
{
    private string gameID;
    private bool testAds;

    private string bannerID;
    private string interstitialID;
    private string rewardedID;

    public override void Initialize(Arguments arguments) {
        gameID = arguments.Get<string>(ArgumentKey.GAME_ID);
        testAds = arguments.Get<bool>(ArgumentKey.TEST_ADS);

        bannerID = arguments.Get<string>(ArgumentKey.BANNER_HIGH_PRIORITY);
        interstitialID = arguments.Get<string>(ArgumentKey.INTERSTITIAL_HIGH_PRIORITY);
        rewardedID = arguments.Get<string>(ArgumentKey.REWARD_HIGH_PRIORITY);

        Advertisement.AddListener(this);
        Advertisement.Initialize(gameID, testAds);
    }


    public override void ShowBanner()
    {
        StartCoroutine(ShowBannerWhenInitialized());
    }
    private IEnumerator ShowBannerWhenInitialized()
    {
        while (!BannerIsReady())
        {
            yield return new WaitForSecondsRealtime(0.01f);
        }
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show(bannerID);
    }
    public override void HideBanner()
    {
        Advertisement.Banner.Hide(false);
    }
    public override void DestroyBanner()
    {
        Advertisement.Banner.Hide(true);
    }
    public override bool BannerIsReady() => Advertisement.Banner.isLoaded;




    public override void ShowInterstitial()
    {
        if(InterstitialIsReady())
            Advertisement.Show(interstitialID);
    }
    public override bool InterstitialIsReady() => Advertisement.IsReady(interstitialID);


    public override void ShowRewardedAd()
    {
        if(RewardedAdIsReady())
            Advertisement.Show(rewardedID);
    }
    public override bool RewardedAdIsReady() => Advertisement.IsReady(rewardedID);


    public void OnUnityAdsReady(string placementId)
    {
        //App.AppendDebugText($"<OnUnityAdsReady {placementId}>");
    }

    public void OnUnityAdsDidError(string message)
    {
        //App.AppendDebugText($"<OnUnityAdsDidError {message}>");
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        //App.AppendDebugText($"<OnUnityAdsDidStart {placementId}>");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        //App.AppendDebugText($"<OnUnityAdsDidFinish {placementId} {showResult}>");
        if (placementId == rewardedID)
            switch (showResult)
            {
                case ShowResult.Finished:
                    this.OnUserEarnedReward?.Invoke();
;                    break;
                case ShowResult.Skipped:
                    this.OnRewardedAdClosed?.Invoke();
                    break;
                case ShowResult.Failed:
                    this.OnRewardedAdClosed?.Invoke();
                    break;
            }
    }

    public void OnDestroy()
    {
        Advertisement.RemoveListener(this);
    }
}
