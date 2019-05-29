using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasModel : Model
{
    public Stack<PanelView> Panels { get; set; }

    public override void Initialize()
    {
        Panels = new Stack<PanelView>();
    }
}
