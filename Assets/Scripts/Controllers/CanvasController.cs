using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using System.Reflection;

public class CanvasController : Controller
{
    public override void Initialize()
    {
        ShowPanel(App.AppModel.CanvasModel.panel1Prefab);
        ShowPanel(App.AppModel.CanvasModel.panel2Prefab);
    }

    public void ShowPanel(GameObject panelPrefab)
    {
        GameObject panel = Instantiate(panelPrefab, App.AppView.CanvasView.transform);
        panel.GetComponent<PanelViewController>().Initialize();

        RectTransform panelRectTransform = panel.GetComponent<RectTransform>();
        panelRectTransform.anchorMin = Vector2.zero;
        panelRectTransform.anchorMax = Vector2.one;

        panelRectTransform.offsetMin = Vector2.zero;
        panelRectTransform.offsetMax = Vector2.zero;

        Canvas panelCanvas = panel.GetComponent<Canvas>();
        Canvas prevCanvas = default;

        if (App.AppModel.CanvasModel.Panels.Count > 0)
        {
            foreach (GameObject temp in App.AppModel.CanvasModel.Panels)
                temp.SetActive(false);

            prevCanvas = App.AppModel.CanvasModel.Panels.Peek().GetComponent<Canvas>();
        }
        else
        {
            prevCanvas = App.AppView.CanvasView.GetComponent<Canvas>();
        }
        panelCanvas.sortingOrder = prevCanvas.sortingOrder + 1;
        App.AppModel.CanvasModel.Panels.Push(panel);
    }
    public void HidePanel(GameObject panel)
    {
        GameObject temp = App.AppModel.CanvasModel.Panels.Peek();
        if(temp == panel)
        {
            if(App.AppModel.CanvasModel.Panels.Count == 1)
                return;

            App.AppModel.CanvasModel.Panels.Pop();
            Destroy(panel);
            App.AppModel.CanvasModel.Panels.Peek().SetActive(true);
        }
    }

    /*private PanelViewController GetPanelPrefab(PanelName panelName)
    {
        // Get prefab dynamically, based on public fields set from Unity
        // You can use private fields with SerializeField attribute too
        var fields = App
            .AppModel
            .CanvasModel
            .GetType()
            .GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        foreach (var field in fields)
        {
            var prefab = field.GetValue(this) as T;
            if (prefab != null)
            {
                return prefab;
            }
        }

        throw new MissingReferenceException("Prefab not found for type " + typeof(T));
    }*/


    #region Event handlers

    public override void OnInitialNotification(EventName eventName, params object[] data)
    {
    }

    public override void OnLateNotification(EventName eventName, params object[] data)
    {
    }

    public override void OnNotification(EventName eventName, params object[] data)
    {

    }

    #endregion
}
