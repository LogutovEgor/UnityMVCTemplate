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
        //App.AppView.NavigationView.GetComponent<Animator>().Play("Empty");
        //App.AppView.NavigationView.GetComponent<Animator>().Play("SwitchOrientation");
        foreach(GameObject page in App.AppModel.NavigationModel.NavigationStack)
        {
            Page temp = page.GetComponent<Page>();
            Destroy(temp.content);
            //
            GameObject pageContentPrefab = App.AppModel.NavigationModel.GetContent(temp.pageName, pageOrientation);
            GameObject pageContent = Instantiate(pageContentPrefab, page.transform);
            temp.content = pageContent;
        }
    }

    public void Pop()
    {
        
    }

    public void Push(PageName pageName)
    {
        GameObject pagePrefab = App.AppModel.NavigationModel.GetPage(pageName);
        GameObject page = Instantiate(pagePrefab, App.AppView.NavigationView.transform);
        page.GetComponent<Page>().Initialize();

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
            prevPage.SetActive(false);
            //prevPage.GetComponent<Animator>().SetTrigger("Hide");
            //prevPanel.GetComponent<PanelViewController>().destroyOnHide = false;
            //prevPanel.SetActive(!prevPanel.GetComponent<PanelViewController>().deactivate);
            prevPageCanvas = prevPage.GetComponent<Canvas>();
        }
        else
            prevPageCanvas = App.AppView.NavigationView.GetComponent<Canvas>();

        pageCanvas.sortingOrder = prevPageCanvas.sortingOrder + 1;
        //
        GameObject pageContentPrefab = App.AppModel.NavigationModel.GetContent(pageName, currentPageOrientation);
        GameObject pageContent = Instantiate(pageContentPrefab, page.transform);
        page.GetComponent<Page>().content = pageContent;

        App.AppModel.NavigationModel.NavigationStack.Push(page);
    }

    public void PopToRoot()
    {

    }
}
