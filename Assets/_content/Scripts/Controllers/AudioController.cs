using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class AudioController : Controller
{
    public override void Initialize(Arguments arguments = null)
    {
        if (initialized)
            return;
        initialized = true;
    }
}
