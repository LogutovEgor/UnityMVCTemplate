using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasView : View
{
    public override void Initialize()
    {
        if (initialized)
            return;
        initialized = true;
    }
}
