using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using System.Linq;

public class NavigationController : Controller, INavigation
{
    public PageOrientation currentPageOrientation;

    public override void Initialize()
    {
        if(UnityEngine.Application.platform != RuntimePlatform.WindowsEditor)
            currentPageOrientation = ToPageOrientation(Input.deviceOrientation);
        else
            currentPageOrientation = PageOrientation.Landscape;
        Push(PageName.Test);
        Push(PageName.Test);
        Push(PageName.Test);
    }

    public void Update()
    {
        if (UnityEngine.Application.platform != RuntimePlatform.WindowsEditor)
        {
            if (currentPageOrientation != ToPageOrientation(Input.deviceOrientation))
            {
                currentPageOrientation = ToPageOrientation(Input.deviceOrientation);
                SwitchOrientation(currentPageOrientation);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (currentPageOrientation == PageOrientation.Portrait)
                    currentPageOrientation = PageOrientation.Landscape;
                else
                    currentPageOrientation = PageOrientation.Portrait;
                SwitchOrientation(currentPageOrientation);
            }
        }
    }

    protected PageOrientation ToPageOrientation(DeviceOrientation deviceOrientation)
    {
        PageOrientation pageOrientation = default;
        if (deviceOrientation == DeviceOrientation.Portrait || deviceOrientation == DeviceOrientation.PortraitUpsideDown)
            pageOrientation = PageOrientation.Portrait;
        else
            pageOrientation = PageOrientation.Landscape;
        return pageOrientation;
    }

    protected void SwitchOrientation(PageOrientation pageOrientation)
    {
        Stack<GameObject> tempNavigationStack = App.AppModel.NavigationModel.NavigationStack;
        tempNavigationStack.Reverse();
        App.AppModel.NavigationModel.NavigationStack = new Stack<GameObject>();
        while(tempNavigationStack.Count > 0)
        {
            GameObject tempPage = tempNavigationStack.Pop();
            Page tempPageComponent = tempPage.GetComponent<Page>();
            Push(tempPageComponent.pageName, tempPageComponent.parameters);
            Destroy(tempPage);
        }
    }

    public void Pop()
    {
        
    }

    public void Push(PageName pageName, params object[] parameters)
    {
        GameObject pagePrefab = App.AppModel.NavigationModel.GetPage(pageName, currentPageOrientation);
        GameObject page = Instantiate(pagePrefab, App.AppView.NavigationView.transform);
        Page pageComponent = page.GetComponent<Page>();
        pageComponent.parameters = parameters;
        pageComponent.Initialize();

        RectTransform panelRectTransform = page.GetComponent<RectTransform>();
        panelRectTransform.anchorMin = Vector2.zero;
        panelRectTransform.anchorMax = Vector2.one;

        panelRectTransform.offsetMin = Vector2.zero;
        panelRectTransform.offsetMax = Vector2.zero;

        Canvas pageCanvas = page.GetComponent<Canvas>();
        Canvas prevPageCanvas = default;

        if (App.AppModel.NavigationModel.NavigationStack.Count > 0)
        {
            //foreach (GameObject temp in App.AppModel.CanvasModel.Panels)
            //if(temp.GetComponent<PanelViewController>().deactivate)
            //temp.SetActive(false);

            GameObject prevPage = App.AppModel.NavigationModel.NavigationStack.Peek();
            Page prevPageComponent = prevPage.GetComponent<Page>();

            prevPage.GetComponent<Animator>().SetTrigger("Hide");
            prevPageComponent.destroyOnHide = false;
            prevPageCanvas = prevPage.GetComponent<Canvas>();
        }
        else
            prevPageCanvas = App.AppView.NavigationView.GetComponent<Canvas>();

        pageCanvas.sortingOrder = prevPageCanvas.sortingOrder + 1;
        App.AppModel.NavigationModel.NavigationStack.Push(page);
    }

    public void PopToRoot()
    {

    }
}
