using System;
using UnityEngine;

public abstract class AdsDependency : MonoBehaviour
{
    public Action OnBannerAdLoaded;
    public abstract void Initialize(Arguments arguments = default);
    public abstract void ShowBanner();
    public abstract void HideBanner();
    public abstract void DestroyBanner();
    public abstract bool BannerIsReady();


    public abstract void ShowInterstitial();
    public abstract bool InterstitialIsReady();

    public Action OnRewardedAdClosed;
    public Action OnUserEarnedReward;
    public abstract void ShowRewardedAd();
    public abstract bool RewardedAdIsReady();

}
