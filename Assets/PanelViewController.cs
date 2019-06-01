using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public abstract class PanelViewController : Controller
{
    public virtual void Hide()
    {
        App.AppController.CanvasController.HidePanel(gameObject);
    }

    public virtual void Back()
    {
        Hide();
    }
}
