using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class Panel1ViewController : PanelViewController
{
    public override void Initialize()
    {
        Debug.Log("Panel1 Init");
    }

    public void ShowPanel2()
    {
        Debug.Log("Show panel2");
        App.AppController.CanvasController.ShowPanel(App.AppModel.CanvasModel.panel2Prefab);
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
