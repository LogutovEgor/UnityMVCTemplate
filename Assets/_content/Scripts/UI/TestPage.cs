using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPage : Page
{
    public override void Initialize(Arguments arguments = null)
    {
        base.Initialize(arguments);
        //

    }

    public void OnBtnClick1()
    {
        App.AppController.AdsController.ShowBanner();
    }
    public void OnBtnClick2()
    {
        App.AppController.AdsController.ShowInterstitial();
    }
}
