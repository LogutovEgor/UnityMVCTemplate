using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioView : View
{
    public AudioSource AudioSource1 { get; private set; }

    public override void Initialize(Arguments arguments = null)
    {
        if (initialized)
            return;
        initialized = true;
        //
        AudioSource1 = GetComponent<AudioSource>();
    }
}
