using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class Panel2ViewController : PanelViewController
{
    public override void Initialize()
    {
        Debug.Log("Panel2 Init");
    }

    public override void OnInitialNotification(EventName eventName, params object[] data)
    {
    }

    public override void OnLateNotification(EventName eventName, params object[] data)
    {
    }

    public override void OnNotification(EventName eventName, params object[] data)
    {
    }
}
