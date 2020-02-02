//using System.Collections;
//using System.Collections.Generic;
//using Enums;
//using UnityEngine;
//using System.Reflection;
//using System.Linq;

//public class CanvasController : Controller
//{
//    public override void Initialize()
//    {
//        if (initialized)
//            return;
//        initialized = true;
//        //
//        //ShowPanel(PanelName.DebugSavePanel);
//    }

//    public void ShowPanel(PanelName panelName)
//    {
//        GameObject panelPrefab = GetPanelPrefab(panelName);
//        GameObject panel = Instantiate(panelPrefab, App.AppView.CanvasView.transform);
//        panel.GetComponent<PanelViewController>().Initialize();

//        RectTransform panelRectTransform = panel.GetComponent<RectTransform>();
//        panelRectTransform.anchorMin = Vector2.zero;
//        panelRectTransform.anchorMax = Vector2.one;

//        panelRectTransform.offsetMin = Vector2.zero;
//        panelRectTransform.offsetMax = Vector2.zero;

//        Canvas panelCanvas = panel.GetComponent<Canvas>();
//        Canvas prevCanvas = default;

//        if (App.AppModel.CanvasModel.Panels.Count > 0)
//        {
//            //foreach (GameObject temp in App.AppModel.CanvasModel.Panels)
//            //if(temp.GetComponent<PanelViewController>().deactivate)
//            //temp.SetActive(false);

//            GameObject prevPanel = App.AppModel.CanvasModel.Panels.Peek();
//            prevPanel.GetComponent<Animator>().SetTrigger("Hide");
//            prevPanel.GetComponent<PanelViewController>().destroyOnHide = false;
//            //prevPanel.SetActive(!prevPanel.GetComponent<PanelViewController>().deactivate);
//            prevCanvas = prevPanel.GetComponent<Canvas>();
//        }
//        else
//            prevCanvas = App.AppView.CanvasView.GetComponent<Canvas>();

//        panelCanvas.sortingOrder = prevCanvas.sortingOrder + 1;
//        App.AppModel.CanvasModel.Panels.Push(panel);
//    }
//    public void HidePanel(GameObject panel)
//    {
//        GameObject temp = App.AppModel.CanvasModel.Panels.Peek();
//        if (temp == panel)
//        {
//            //if (App.AppModel.CanvasModel.Panels.Count == 1)
//                //return;
            
//            App.AppModel.CanvasModel.Panels.Pop();
//            //Destroy(panel);
//            //App.AppModel.CanvasModel.Panels.Peek().SetActive(true);
//            panel.GetComponent<Animator>().SetTrigger("Hide");
//            panel.GetComponent<PanelViewController>().destroyOnHide = true;
//            //panel.GetComponent<Canvas>().sortingOrder -= 2;
//            if (App.AppModel.CanvasModel.Panels.Count > 0)
//            {
//                App.AppModel.CanvasModel.Panels.Peek().GetComponent<Animator>().SetTrigger("Show");
//                App.AppModel.CanvasModel.Panels.Peek().SetActive(true);
//            }
//        }
//    }

//    public GameObject GetPanelPrefab(PanelName panelName)
//    {
//        FieldInfo[] fields = App
//          .AppModel
//          .CanvasModel
//          .GetType()
//          .GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

//        GameObject prefab
//            = fields
//            .First(info => info.Name == panelName.ToString())
//            .GetValue(App.AppModel.CanvasModel) as GameObject;

//        return prefab;
//    }
//}
