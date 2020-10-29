using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyAdsController : AdsDependency
{
    public override bool BannerIsReady() => true;

    public override void DestroyBanner()
    {
    }

    public override void HideBanner() => Debug.Log("DummyAdsController: HideBanner()");
    

    public override void Initialize(Arguments arguments) => Debug.Log("DummyAdsController: Initialize()");

    public override bool InterstitialIsReady() => true;

    public override bool RewardedAdIsReady() => true;

    public override void ShowBanner() => Debug.Log("DummyAdsController: ShowBanner()");
    

    public override void ShowInterstitial() => Debug.Log("DummyAdsController: ShowInterstitial()");


    public override void ShowRewardedAd() { 
        Debug.Log("DummyAdsController: ShowRewardedAd()");
        this.OnUserEarnedReward?.Invoke();
    }
}
