using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class ApplicationController : Controller
{
    public AudioController AudioController { get; private set; }
    public NavigationController NavigationController { get; private set; }
    public AdsController AdsController { get; private set; }

    public override void Initialize(Arguments arguments = default)
    {
        if (initialized)
            return;
        initialized = true;
    }
    protected void Update()
    {
        if (UnityEngine.Application.targetFrameRate != App.AppModel.TargetFramerate)
            UnityEngine.Application.targetFrameRate = App.AppModel.TargetFramerate;
    }
}
