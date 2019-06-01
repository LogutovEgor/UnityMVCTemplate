using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasModel : Model
{
    public GameObject panel1Prefab;
    public GameObject panel2Prefab;

    public Stack<GameObject> Panels { get; set; }

    public override void Initialize()
    {
        Panels = new Stack<GameObject>();
    }
}
