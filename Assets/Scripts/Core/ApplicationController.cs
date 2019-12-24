using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class ApplicationController : Controller
{
    public CanvasController CanvasController { get; private set; }
    public AudioController AudioController { get; private set; }

    public override void Initialize()
    {
        if (initialized)
            return;
        initialized = true;
        //
        //CanvasController = GetComponentInChildren<CanvasController>();
        //CanvasController.Initialize();
        //
        //AudioController = GetComponentInChildren<AudioController>();
        //AudioController.Initialize();
    }
    protected void Update()
    {
        if (UnityEngine.Application.targetFrameRate != App.AppModel.TargetFramerate)
            UnityEngine.Application.targetFrameRate = App.AppModel.TargetFramerate;
    }
}
