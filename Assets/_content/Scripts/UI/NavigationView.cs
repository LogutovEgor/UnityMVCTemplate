using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationView : View
{
    public Canvas Canvas { get; private set; }

    public override void Initialize(Arguments arguments = default)
    {
        Canvas = this.GetComponent<Canvas>();
    }
}
