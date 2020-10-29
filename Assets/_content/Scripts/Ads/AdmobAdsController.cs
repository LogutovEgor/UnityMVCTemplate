//using System.Collections;
//using System.Collections.Generic;
//using Enums;
//using UnityEngine;
//using System;
//using GoogleMobileAds.Api;

//public class AdmobAdsController : AdsDependency
//{
//    [SerializeField]
//    private AdsDummy adsDummy;

//    private float timeToRefreshBanner = 0;
//    public override void Initialize(Arguments arguments = default)
//    {
//        MobileAds.SetiOSAppPauseOnBackground(true);
//        MobileAds.Initialize(initStatus => {
//        });
//        //
//        if (!App.AppModel.SaveModel.NoAds)
//        {
//            RequestInterstitials();
//            RequestBanners();
//        }
//        RequestRewardedAds();
//    }

//    #region Banners
//    protected enum BannerPriority { High, Medium, Low, None }

//    protected BannerView bannerHighPriority;
//    protected BannerView bannerMediumPriority;
//    protected BannerView bannerLowPriority;

//    protected BannerPriority currentBannerPriority = BannerPriority.None;

//    protected void RequestBanners()
//    {
//        AdSize adaptiveSize =
//                AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);

//        if (bannerHighPriority == null)
//        {
//            bannerHighPriority =
//                new BannerView(App.AppModel.AdsModel.AdmobBannerHighPriorityID,
//                               adaptiveSize,
//                               AdPosition.Bottom);

//            bannerHighPriority.OnAdLoaded += HandleBannerHighPriorityLoaded;
//            bannerHighPriority.OnAdFailedToLoad += HandleBannerHighPriorityFailedToLoad;
//            bannerHighPriority.OnAdClosed += HandleBannerHighPriorityClosed;
//            bannerHighPriority.OnAdLeavingApplication += HandleBannerHighPriorityLeftApplication;
//            // Create an empty ad request.
//            AdRequest requestBannerHighPriority = new AdRequest.Builder().Build();
//            // Load the banner with the request.
//            bannerHighPriority.LoadAd(requestBannerHighPriority);
//        }
//        if (bannerMediumPriority == null)
//        {
//            bannerMediumPriority =
//                new BannerView(App.AppModel.AdsModel.AdmobBannerMediumPriorityID,
//                               adaptiveSize,
//                               AdPosition.Bottom);

//            bannerMediumPriority.OnAdLoaded += HandleBannerMediumPriorityLoaded;
//            bannerMediumPriority.OnAdFailedToLoad += HandleBannerMediumPriorityFailedToLoad;
//            bannerMediumPriority.OnAdClosed += HandleBannerMediumPriorityClosed;
//            bannerMediumPriority.OnAdLeavingApplication += HandleBannerMediumPriorityLeftApplication;
//            // Create an empty ad request.
//            AdRequest requestBannerMediumPriority = new AdRequest.Builder().Build();
//            // Load the banner with the request.
//            bannerMediumPriority.LoadAd(requestBannerMediumPriority);
//        }
//        if (bannerLowPriority == null)
//        {
//            bannerLowPriority =
//                new BannerView(App.AppModel.AdsModel.AdmobBannerLowPriorityID,
//                               adaptiveSize,
//                               AdPosition.Bottom);

//            bannerLowPriority.OnAdLoaded += HandleBannerLowPriorityLoaded;
//            bannerLowPriority.OnAdFailedToLoad += HandleBannerLowPriorityFailedToLoad;
//            bannerLowPriority.OnAdClosed += HandleBannerLowPriorityClosed;
//            bannerLowPriority.OnAdLeavingApplication += HandleBannerLowPriorityLeftApplication;
//            // Create an empty ad request.
//            AdRequest requestBannerLowPriority = new AdRequest.Builder().Build();
//            // Load the banner with the request.
//            bannerLowPriority.LoadAd(requestBannerLowPriority);
//        }
//    }
//    //Banner high priority
//    public void HandleBannerHighPriorityLoaded(object sender, EventArgs args)
//    {
//        this.OnBannerAdLoaded?.Invoke();
//        if (currentBannerPriority == BannerPriority.None ||
//           currentBannerPriority == BannerPriority.Medium ||
//           currentBannerPriority == BannerPriority.Low)
//        {
//            currentBannerPriority = BannerPriority.High;
//        }
//    }

//    public void HandleBannerHighPriorityFailedToLoad(object sender, AdFailedToLoadEventArgs args)
//    {
//        bannerHighPriority?.Destroy();
//        bannerHighPriority = null;
//        RequestBanners();
//    }

//    public void HandleBannerHighPriorityClosed(object sender, EventArgs args)
//    {
//        bannerHighPriority?.Destroy();
//        bannerHighPriority = null;
//        RequestBanners();
//    }

//    public void HandleBannerHighPriorityLeftApplication(object sender, EventArgs args)
//    {
//        bannerHighPriority?.Destroy();
//        bannerHighPriority = null;
//        RequestBanners();
//    }
//    //
//    //Banner medium priority
//    public void HandleBannerMediumPriorityLoaded(object sender, EventArgs args)
//    {
//        this.OnBannerAdLoaded?.Invoke();
//        if (currentBannerPriority == BannerPriority.None || currentBannerPriority == BannerPriority.Low)
//            currentBannerPriority = BannerPriority.Medium;
//    }

//    public void HandleBannerMediumPriorityFailedToLoad(object sender, AdFailedToLoadEventArgs args)
//    {
//        bannerMediumPriority?.Destroy();
//        bannerMediumPriority = null;
//        RequestBanners();
//    }

//    public void HandleBannerMediumPriorityClosed(object sender, EventArgs args)
//    {
//        bannerMediumPriority?.Destroy();
//        bannerMediumPriority = null;
//        RequestBanners();
//    }

//    public void HandleBannerMediumPriorityLeftApplication(object sender, EventArgs args)
//    {
//        bannerMediumPriority?.Destroy();
//        bannerMediumPriority = null;
//        RequestBanners();
//    }
//    //
//    //Banner low priority
//    public void HandleBannerLowPriorityLoaded(object sender, EventArgs args)
//    {
//        this.OnBannerAdLoaded?.Invoke();
//        if (currentBannerPriority == BannerPriority.None)
//            currentBannerPriority = BannerPriority.Low;
//    }

//    public void HandleBannerLowPriorityFailedToLoad(object sender, AdFailedToLoadEventArgs args)
//    {
//        bannerLowPriority?.Destroy();
//        bannerLowPriority = null;
//        RequestBanners();
//    }

//    public void HandleBannerLowPriorityClosed(object sender, EventArgs args)
//    {
//        bannerLowPriority?.Destroy();
//        bannerLowPriority = null;
//        RequestBanners();
//    }

//    public void HandleBannerLowPriorityLeftApplication(object sender, EventArgs args)
//    {
//        bannerLowPriority?.Destroy();
//        bannerLowPriority = null;
//        RequestBanners();
//    }
//    //

//    public override void ShowBanner()
//    {
//        switch (currentBannerPriority)
//        {
//            case BannerPriority.High:
//                //App.debugText.text += "<show banner high>";
//                timeToRefreshBanner = App.AppModel.AdsModel.RefreshBannerTime;
//                bannerHighPriority.Show();
//                break;
//            case BannerPriority.Medium:
//                //App.debugText.text += "<show banner medium>";
//                timeToRefreshBanner = App.AppModel.AdsModel.RefreshBannerTime;
//                bannerMediumPriority.Show();
//                break;
//            case BannerPriority.Low:
//                //App.debugText.text += "<show banner low>";
//                timeToRefreshBanner = App.AppModel.AdsModel.RefreshBannerTime;
//                bannerLowPriority.Show();
//                break;
//        }
//    }
//    public override void HideBanner()
//    {
//        bannerHighPriority?.Hide();
//        bannerMediumPriority?.Hide();
//        bannerLowPriority?.Hide();
//    }
//    public override void DestroyBanner()
//    {
//        bannerHighPriority?.Hide();
//        bannerHighPriority?.Destroy();
//        bannerHighPriority = default;

//        bannerMediumPriority?.Hide();
//        bannerMediumPriority?.Destroy();
//        bannerMediumPriority = default;

//        bannerLowPriority?.Hide();
//        bannerLowPriority?.Destroy();
//        bannerLowPriority = default;
//    }

//    public override bool BannerIsReady() => currentBannerPriority != BannerPriority.None;
//    //UnityEngine.Application.internetReachability != NetworkReachability.NotReachable;


//    private void Update()
//    {
//        if (App.AppModel.SaveModel.NoAds)
//            return;
//        timeToRefreshBanner -= Time.unscaledDeltaTime;
//        if (timeToRefreshBanner <= 0)
//        {
//            //App.debugText.text += "<refresh banner>";
//            switch (currentBannerPriority)
//            {
//                case BannerPriority.High:
//                    bannerHighPriority?.Destroy();
//                    bannerHighPriority = null;
//                    RequestBanners();
//                    break;
//                case BannerPriority.Medium:
//                    bannerMediumPriority?.Destroy();
//                    bannerMediumPriority = null;
//                    RequestBanners();
//                    break;
//                case BannerPriority.Low:
//                    bannerLowPriority?.Destroy();
//                    bannerLowPriority = null;
//                    RequestBanners();
//                    break;
//            }
//            ShowBanner();
//        }

//    }

//    #endregion
//    #region Interstitials

//    protected InterstitialAd interstitialHighPriority;
//    protected InterstitialAd interstitialMediumPriority;
//    protected InterstitialAd interstitialLowPriority;

//    protected void RequestInterstitials()
//    {
//        // Initialize an InterstitialAd.
//        if (interstitialHighPriority == null)
//        {
//            interstitialHighPriority =
//                new InterstitialAd(App.AppModel.AdsModel.AdmobInterstitialHighPriorityID);
//            //////////////////////
//            // Called when an ad request has successfully loaded.
//            //this.interstitial.OnAdLoaded += HandleOnAdLoaded;
//            // Called when an ad request failed to load.
//            interstitialHighPriority.OnAdFailedToLoad +=
//                HandleOnInterstitialHighPriorityFailedToLoad;
//            // Called when an ad is shown.
//            //this.interstitial.OnAdOpening += HandleOnAdOpened;
//            // Called when the ad is closed.
//            interstitialHighPriority.OnAdClosed +=
//                HandleOnInterstitialHighPriorityClosed;
//            // Called when the ad click caused the user to leave the application.
//            interstitialHighPriority.OnAdLeavingApplication +=
//                HandleOnInterstitialHighPriorityLeavingApplication;
//            // Create an empty ad request.
//            AdRequest requestHighPriority = new AdRequest.Builder().Build();
//            // Load the interstitial with the request.
//            interstitialHighPriority.LoadAd(requestHighPriority);
//            //////////////////////
//        }
//        //
//        if (interstitialMediumPriority == null)
//        {
//            interstitialMediumPriority =
//                new InterstitialAd(App.AppModel.AdsModel.AdmobInterstitialMediumPriorityID);
//            //////////////////////
//            // Called when an ad request has successfully loaded.
//            //this.interstitial.OnAdLoaded += HandleOnAdLoaded;
//            // Called when an ad request failed to load.
//            interstitialMediumPriority.OnAdFailedToLoad +=
//                HandleOnInterstitialMediumPriorityFailedToLoad;
//            // Called when an ad is shown.
//            //this.interstitial.OnAdOpening += HandleOnAdOpened;
//            // Called when the ad is closed.
//            interstitialMediumPriority.OnAdClosed +=
//                HandleOnInterstitialMediumPriorityClosed;
//            // Called when the ad click caused the user to leave the application.
//            interstitialMediumPriority.OnAdLeavingApplication +=
//                HandleOnInterstitialMediumPriorityLeavingApplication;
//            // Create an empty ad request.
//            AdRequest requestMediumPriority = new AdRequest.Builder().Build();
//            // Load the interstitial with the request.
//            interstitialMediumPriority.LoadAd(requestMediumPriority);
//            //////////////////////
//        }
//        //
//        if (interstitialLowPriority == null)
//        {
//            interstitialLowPriority =
//                new InterstitialAd(App.AppModel.AdsModel.AdmobInterstitialLowPriorityID);
//            // Called when an ad request has successfully loaded.
//            //this.interstitial.OnAdLoaded += HandleOnAdLoaded;
//            // Called when an ad request failed to load.
//            interstitialLowPriority.OnAdFailedToLoad +=
//                HandleOnInterstitialLowPriorityFailedToLoad;
//            // Called when an ad is shown.
//            //this.interstitial.OnAdOpening += HandleOnAdOpened;
//            // Called when the ad is closed.
//            interstitialLowPriority.OnAdClosed +=
//                HandleOnInterstitialLowPriorityClosed;
//            // Called when the ad click caused the user to leave the application.
//            interstitialLowPriority.OnAdLeavingApplication +=
//                HandleOnInterstitialLowPriorityLeavingApplication;
//            // Create an empty ad request.
//            AdRequest requestLowPriority = new AdRequest.Builder().Build();
//            // Load the interstitial with the request.
//            interstitialLowPriority.LoadAd(requestLowPriority);
//            //////////////////////
//        }
//    }

//    //High priority
//    protected void HandleOnInterstitialHighPriorityLeavingApplication(object sender, EventArgs e)
//    {
//        interstitialHighPriority?.Destroy();
//        interstitialHighPriority = null;
//        RequestInterstitials();
//    }

//    protected void HandleOnInterstitialHighPriorityClosed(object sender, EventArgs e)
//    {
//        interstitialHighPriority?.Destroy();
//        interstitialHighPriority = null;
//        RequestInterstitials();
//    }

//    protected void HandleOnInterstitialHighPriorityFailedToLoad(object sender, AdFailedToLoadEventArgs e)
//    {
//        interstitialHighPriority?.Destroy();
//        interstitialHighPriority = null;
//        RequestInterstitials();
//    }
//    //
//    //Medium priority
//    protected void HandleOnInterstitialMediumPriorityLeavingApplication(object sender, EventArgs e)
//    {
//        interstitialMediumPriority?.Destroy();
//        interstitialMediumPriority = null;
//        RequestInterstitials();
//    }

//    protected void HandleOnInterstitialMediumPriorityClosed(object sender, EventArgs e)
//    {
//        interstitialMediumPriority?.Destroy();
//        interstitialMediumPriority = null;
//        RequestInterstitials();
//    }

//    protected void HandleOnInterstitialMediumPriorityFailedToLoad(object sender, AdFailedToLoadEventArgs e)
//    {
//        interstitialMediumPriority?.Destroy();
//        interstitialMediumPriority = null;
//        RequestInterstitials();
//    }
//    //
//    //Low priority
//    protected void HandleOnInterstitialLowPriorityLeavingApplication(object sender, EventArgs e)
//    {
//        interstitialLowPriority?.Destroy();
//        interstitialLowPriority = null;
//        RequestInterstitials();
//    }

//    protected void HandleOnInterstitialLowPriorityClosed(object sender, EventArgs e)
//    {
//        interstitialLowPriority?.Destroy();
//        interstitialLowPriority = null;
//        RequestInterstitials();
//    }

//    protected void HandleOnInterstitialLowPriorityFailedToLoad(object sender, AdFailedToLoadEventArgs e)
//    {
//        interstitialLowPriority?.Destroy();
//        interstitialLowPriority = null;
//        RequestInterstitials();
//    }
//    //
//    public override void ShowInterstitial()
//    {
//        if ((interstitialHighPriority != null) && InterstitialHighPriorityIsReady)
//        {
//            //adsDummy.StartDummy(ShowInterstitialHighPriority, OnInterstitialTransitionHide);
//            ShowInterstitialHighPriority();
//            return;
//        }
//        if ((interstitialMediumPriority != null) && InterstitialMediumPriorityIsReady)
//        {
//            //adsDummy.StartDummy(ShowInterstitialMediumPriority, OnInterstitialTransitionHide);
//            ShowInterstitialMediumPriority();
//            return;
//        }
//        if ((interstitialLowPriority != null) && InterstitialLowPriorityIsReady)
//        {
//            //adsDummy.StartDummy(ShowInterstitialLowPriority, OnInterstitialTransitionHide);
//            ShowInterstitialLowPriority();
//            return;
//        }
//    }

//    private void ShowInterstitialHighPriority()
//    {
//        interstitialHighPriority.Show();
//        interstitialHighPriority = null;
//        RequestInterstitials();
//    }

//    private void ShowInterstitialMediumPriority()
//    {
//        interstitialMediumPriority.Show();
//        interstitialMediumPriority = null;
//        RequestInterstitials();
//    }

//    private void ShowInterstitialLowPriority()
//    {
//        interstitialLowPriority.Show();
//        interstitialLowPriority = null;
//        RequestInterstitials();
//    }

//    //private void OnInterstitialTransitionHide()
//    //{
//    //    Application.Instance.Notify(EventName.OnInterstitialTransitionClosed);
//    //}

//    protected bool InterstitialHighPriorityIsReady => //interstitialHighPriority != null && 
//        interstitialHighPriority.IsLoaded();
//    protected bool InterstitialMediumPriorityIsReady => //interstitialMediumPriority != null && 
//        interstitialMediumPriority.IsLoaded();
//    protected bool InterstitialLowPriorityIsReady => //interstitialLowPriority!= null && 
//        interstitialLowPriority.IsLoaded();

//    public override bool InterstitialIsReady() =>
//        InterstitialHighPriorityIsReady ||
//        InterstitialMediumPriorityIsReady ||
//        InterstitialLowPriorityIsReady;


//    #endregion

//    #region RewardedAds

//    RewardedAd rewardedAdHighPriority;
//    RewardedAd rewardedAdMediumPriority;
//    RewardedAd rewardedAdLowPriority;

//    protected void RequestRewardedAds()
//    {
//        if (rewardedAdHighPriority == null)
//        {
//            rewardedAdHighPriority = new RewardedAd(App.AppModel.AdsModel.AdmobRewardedAdHighPriorityID);
//            // Called when an ad request has successfully loaded.
//            ///this.rewardedAdHighPriority.OnAdLoaded += HandleRewardedAdLoaded;
//            // Called when an ad request failed to load.
//            rewardedAdHighPriority.OnAdFailedToLoad += HandleRewardedAdHighPriorityFailedToLoad;
//            // Called when an ad is shown.
//            //this.rewardedAdHighPriority.OnAdOpening += HandleRewardedAdOpening;
//            // Called when an ad request failed to show.
//            rewardedAdHighPriority.OnAdFailedToShow += HandleRewardedAdHighPriorityFailedToShow;
//            // Called when the user should be rewarded for interacting with the ad.
//            rewardedAdHighPriority.OnUserEarnedReward += HandleUserEarnedRewardAdHighPriority;
//            // Called when the ad is closed.
//            rewardedAdHighPriority.OnAdClosed += HandleRewardedAdHighPriorityClosed;
//            // Create an empty ad request.
//            AdRequest request = new AdRequest.Builder().Build();
//            // Load the rewarded ad with the request.
//            rewardedAdHighPriority.LoadAd(request);
//            // App.debugText.text += "<RH>";
//        }
//        //
//        if (rewardedAdMediumPriority == null)
//        {
//            rewardedAdMediumPriority =
//                new RewardedAd(App.AppModel.AdsModel.AdmobRewardedAdMediumPriorityID);
//            // Called when an ad request has successfully loaded.
//            ///this.rewardedAdHighPriority.OnAdLoaded += HandleRewardedAdLoaded;
//            // Called when an ad request failed to load.
//            rewardedAdMediumPriority.OnAdFailedToLoad += HandleRewardedAdMediumPriorityFailedToLoad;
//            // Called when an ad is shown.
//            //this.rewardedAdHighPriority.OnAdOpening += HandleRewardedAdOpening;
//            // Called when an ad request failed to show.
//            rewardedAdMediumPriority.OnAdFailedToShow += HandleRewardedAdMediumPriorityFailedToShow;
//            // Called when the user should be rewarded for interacting with the ad.
//            rewardedAdMediumPriority.OnUserEarnedReward += HandleUserEarnedRewardAdMediumPriority;
//            // Called when the ad is closed.
//            rewardedAdMediumPriority.OnAdClosed += HandleRewardedAdMediumPriorityClosed;
//            // Create an empty ad request.
//            AdRequest request = new AdRequest.Builder().Build();
//            // Load the rewarded ad with the request.
//            rewardedAdMediumPriority.LoadAd(request);
//            //App.debugText.text += "<RM>";
//        }
//        //
//        if (rewardedAdLowPriority == null)
//        {
//            rewardedAdLowPriority =
//                new RewardedAd(App.AppModel.AdsModel.AdmobRewardedAdLowPriorityID);
//            // Called when an ad request has successfully loaded.
//            ///this.rewardedAdHighPriority.OnAdLoaded += HandleRewardedAdLoaded;
//            // Called when an ad request failed to load.
//            rewardedAdLowPriority.OnAdFailedToLoad += HandleRewardedAdLowPriorityFailedToLoad;
//            // Called when an ad is shown.
//            //this.rewardedAdHighPriority.OnAdOpening += HandleRewardedAdOpening;
//            // Called when an ad request failed to show.
//            rewardedAdLowPriority.OnAdFailedToShow += HandleRewardedAdLowPriorityFailedToShow;
//            // Called when the user should be rewarded for interacting with the ad.
//            rewardedAdLowPriority.OnUserEarnedReward += HandleUserEarnedRewardAdLowPriority;
//            // Called when the ad is closed.
//            rewardedAdLowPriority.OnAdClosed += HandleRewardedAdLowPriorityClosed;
//            // Create an empty ad request.
//            AdRequest request = new AdRequest.Builder().Build();
//            // Load the rewarded ad with the request.
//            rewardedAdLowPriority.LoadAd(request);
//            // App.debugText.text += "<RL>";
//        }
//    }

//    public void HandleRewardedAdHighPriorityFailedToLoad(object sender, AdErrorEventArgs args)
//    {
//        rewardedAdHighPriority = null;
//        RequestRewardedAds();
//    }

//    public void HandleRewardedAdHighPriorityFailedToShow(object sender, AdErrorEventArgs args)
//    {
//        rewardedAdHighPriority = null;
//        RequestRewardedAds();
//    }

//    public void HandleRewardedAdHighPriorityClosed(object sender, EventArgs args)
//    {
//        rewardedAdHighPriority = null;
//        RequestRewardedAds();
//        this.OnRewardedAdClosed?.Invoke();
//    }

//    public void HandleUserEarnedRewardAdHighPriority(object sender, Reward args)
//    {
//        string type = args.Type;
//        double amount = args.Amount;
//        //
//        rewardedAdHighPriority = null;
//        RequestRewardedAds();
//        //
//        this.OnUserEarnedReward?.Invoke();
//    }
//    //////////////
//    public void HandleRewardedAdMediumPriorityFailedToLoad(object sender, AdErrorEventArgs args)
//    {
//        rewardedAdMediumPriority = null;
//        RequestRewardedAds();
//    }

//    public void HandleRewardedAdMediumPriorityFailedToShow(object sender, AdErrorEventArgs args)
//    {
//        rewardedAdMediumPriority = null;
//        RequestRewardedAds();
//    }

//    public void HandleRewardedAdMediumPriorityClosed(object sender, EventArgs args)
//    {
//        rewardedAdMediumPriority = null;
//        RequestRewardedAds();
//        this.OnRewardedAdClosed?.Invoke();
//    }

//    public void HandleUserEarnedRewardAdMediumPriority(object sender, Reward args)
//    {
//        string type = args.Type;
//        double amount = args.Amount;
//        //
//        rewardedAdMediumPriority = null;
//        RequestRewardedAds();
//        //
//        this.OnUserEarnedReward?.Invoke();
//    }
//    //////////
//    public void HandleRewardedAdLowPriorityFailedToLoad(object sender, AdErrorEventArgs args)
//    {
//        rewardedAdLowPriority = null;
//        RequestRewardedAds();
//    }

//    public void HandleRewardedAdLowPriorityFailedToShow(object sender, AdErrorEventArgs args)
//    {
//        rewardedAdLowPriority = null;
//        RequestRewardedAds();
//    }

//    public void HandleRewardedAdLowPriorityClosed(object sender, EventArgs args)
//    {
//        rewardedAdLowPriority = null;
//        RequestRewardedAds();
//        this.OnRewardedAdClosed?.Invoke();
//    }

//    public void HandleUserEarnedRewardAdLowPriority(object sender, Reward args)
//    {
//        string type = args.Type;
//        double amount = args.Amount;
//        //
//        rewardedAdLowPriority = null;
//        RequestRewardedAds();
//        //
//        this.OnUserEarnedReward?.Invoke();
//    }


//    public override void ShowRewardedAd()
//    {
//        if ((rewardedAdHighPriority != null) && RewardedAdHighPriorityIsReady)
//        {
//            ShowRewardedAdHighPriority();
//            return;
//        }
//        if ((rewardedAdMediumPriority != null) && RewardedAdMediumPriorityIsReady)
//        {
//            ShowRewardedAdMediumPriority();
//            return;
//        }
//        if ((rewardedAdLowPriority != null) && RewardedAdLowPriorityIsReady)
//        {
//            ShowRewardedAdLowPriority();
//            return;
//        }
//    }

//    private void ShowRewardedAdHighPriority()
//    {
//        rewardedAdHighPriority.Show();
//        rewardedAdHighPriority = null;
//        RequestRewardedAds();
//    }

//    private void ShowRewardedAdMediumPriority()
//    {
//        rewardedAdMediumPriority.Show();
//        rewardedAdMediumPriority = null;
//        RequestRewardedAds();
//    }

//    private void ShowRewardedAdLowPriority()
//    {
//        rewardedAdLowPriority.Show();
//        rewardedAdLowPriority = null;
//        RequestRewardedAds();
//    }

//    protected bool RewardedAdHighPriorityIsReady => rewardedAdHighPriority != null && rewardedAdHighPriority.IsLoaded();
//    protected bool RewardedAdMediumPriorityIsReady => rewardedAdMediumPriority != null && rewardedAdMediumPriority.IsLoaded();
//    protected bool RewardedAdLowPriorityIsReady => rewardedAdLowPriority != null && rewardedAdLowPriority.IsLoaded();


//    public override bool RewardedAdIsReady() =>
//        RewardedAdHighPriorityIsReady ||
//        RewardedAdMediumPriorityIsReady ||
//        RewardedAdLowPriorityIsReady;

//    #endregion
//    public override void OnInitialNotification(EventName eventName, params object[] paramsData)
//    {
//    }

//    public override void OnNotification(EventName eventName, params object[] paramsData)
//    {
//    }

//    public override void OnLateNotification(EventName eventName, params object[] paramsData)
//    {
//    }
//}
