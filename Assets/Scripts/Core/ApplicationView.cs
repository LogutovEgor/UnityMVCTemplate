using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationView : View
{
    //public CanvasView CanvasView { get; private set; }
    public AudioView AudioView { get; private set; }
    public NavigationView NavigationView { get; private set; }

    public override void Initialize()
    {
        if (initialized)
            return;
        initialized = true;
    }
}
