using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationModel : Model
{
    public SaveModel SaveModel { get; private set; }
    public CanvasModel CanvasModel { get; private set; }
    public AudioModel AudioModel { get; private set; }

    public int TargetFramerate;// { get; set; }
    public bool CLI;

    public override void Initialize()
    {
        if (initialized)
            return;
        initialized = true;
        //
        //SaveModel = GetComponentInChildren<SaveModel>();
        //SaveModel.Initialize();
        //
        //CanvasModel = GetComponentInChildren<CanvasModel>();
        //CanvasModel.Initialize();
        //
        //AudioModel = GetComponentInChildren<AudioModel>();
        //AudioModel.Initialize();
        //
        TargetFramerate = 60;
    }
}
