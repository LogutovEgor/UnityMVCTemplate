using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioModel : Model
{
    public AudioClip AudioClip1 { get; private set; }

    public override void Initialize()
    {
        if (initialized)
            return;
        initialized = true;
    }
}
