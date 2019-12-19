using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasModel : Model
{
    public GameObject DebugSavePanel;

    public Stack<GameObject> Panels { get; set; }

    public override void Initialize()
    {
        if (initialized)
            return;
        initialized = true;
        //
        Panels = new Stack<GameObject>();
    }
}
