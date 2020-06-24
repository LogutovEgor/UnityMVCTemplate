using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationView : View
{
    public NavigationView NavigationView { get; private set; }
    public AudioView AudioView { get; private set; }

    public override void Initialize(Arguments arguments = default)
    {
        if (initialized)
            return;
        initialized = true;
        //
    }
}
