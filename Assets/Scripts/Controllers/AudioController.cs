using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class AudioController : Controller
{
    public override void Initialize()
    {
        if (initialized)
            return;
        initialized = true;
    }
}
