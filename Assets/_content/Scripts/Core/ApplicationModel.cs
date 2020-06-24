using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationModel : Model
{
    public SaveModel SaveModel { get; private set; }
    public NavigationModel NavigationModel { get; private set; }
    public AudioModel AudioModel { get; private set; }

    public int TargetFramerate { get; set; }

    public override void Initialize(Arguments arguments = default)
    {
        if (initialized)
            return;
        initialized = true;
        //
        TargetFramerate = 120;
    }
}
