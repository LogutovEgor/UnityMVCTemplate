using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using System.Linq;

public class NavigationController : Controller, INavigation
{
    public PageOrientation currentPageOrientation;

    public override void Initialize(Arguments arguments = default)
    {
        //if(UnityEngine.Application.platform != RuntimePlatform.WindowsEditor)
        //    currentPageOrientation = ToPageOrientation(Input.deviceOrientation);
        //else
        //currentPageOrientation = PageOrientation.Landscape;
        Push(PageName.TestPage);
    }

    public void Update()
    {
        //if (UnityEngine.Application.platform != RuntimePlatform.WindowsEditor)
        //{
        //    if (currentPageOrientation != ToPageOrientation(Input.deviceOrientation))
        //    {
        //        currentPageOrientation = ToPageOrientation(Input.deviceOrientation);
        //        SwitchOrientation(currentPageOrientation);

        //    }
        //}
        //else
        //{
        //    if (Input.GetKeyDown(KeyCode.Space))
        //    {
        //        if (currentPageOrientation == PageOrientation.Portrait)
        //            currentPageOrientation = PageOrientation.Landscape;
        //        else
        //            currentPageOrientation = PageOrientation.Portrait;
        //        SwitchOrientation(currentPageOrientation);
        //    }
        //}
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
        //
        if (pageOrientation == PageOrientation.Landscape)
            Screen.orientation = ScreenOrientation.Landscape;
        else if (pageOrientation == PageOrientation.Portrait)
            Screen.orientation = ScreenOrientation.Portrait;
        //
        Stack<GameObject> tempNavigationStack = App.AppModel.NavigationModel.NavigationStack;
        tempNavigationStack.Reverse();
        App.AppModel.NavigationModel.NavigationStack = new Stack<GameObject>();
        while (tempNavigationStack.Count > 0)
        {
            GameObject tempPage = tempNavigationStack.Pop();
            Page tempPageComponent = tempPage.GetComponent<Page>();
            Push(tempPageComponent.pageName, tempPageComponent.arguments);
            Destroy(tempPage);
        }
    }

    //public void Pop()
    //{
    //    if (App.AppModel.NavigationModel.NavigationStack.Count == 1)
    //        return;

    //    GameObject page = App.AppModel.NavigationModel.NavigationStack.Pop();
    //    Page pageComponent = page.GetComponent<Page>();

    //    if (pageComponent.playAnim)
    //    {
    //        pageComponent.destroyOnHide = true;
    //        pageComponent.animator.Play("Hide");
    //    }
    //    else
    //        Destroy(page);

    //    if (App.AppModel.NavigationModel.NavigationStack.Count > 0)
    //    {
    //        GameObject prevPage = App.AppModel.NavigationModel.NavigationStack.Peek();
    //        Page prevPageComponent = prevPage.GetComponent<Page>();

    //        prevPage.SetActive(true);
    //        if (prevPageComponent.playAnim)
    //            prevPageComponent.animator.Play("Show");
    //        //prevPage.SetActive(true);
    //    }
    //}

    public void Pop(int count = 1)
    {
        if (App.AppModel.NavigationModel.NavigationStack.Count == 1)
            return;

        for (int i = 1; i <= count; i++)
        {
            GameObject page = App.AppModel.NavigationModel.NavigationStack.Pop();
            Page pageComponent = page.GetComponent<Page>();

            if (i == 1 && pageComponent.playAnim)
            {
                pageComponent.destroyOnHide = true;
                pageComponent.animator.Play("Hide");
            }
            else
                Destroy(page);

            if (App.AppModel.NavigationModel.NavigationStack.Count > 0)
            {
                GameObject prevPage = App.AppModel.NavigationModel.NavigationStack.Peek();
                Page prevPageComponent = prevPage.GetComponent<Page>();
                prevPageComponent.OnReturn();
                prevPage.SetActive(true);
                if (i == count && prevPageComponent.playAnim)
                    prevPageComponent.animator.Play("Show");
                //prevPage.SetActive(true);
            }
        }
    }


    public void Push(PageName pageName, Arguments arguments = default)
    {

        GameObject pagePrefab = App.AppModel.NavigationModel.GetPage(pageName, currentPageOrientation);
        GameObject page = Instantiate(pagePrefab, App.AppView.NavigationView.transform);
        Page pageComponent = page.GetComponent<Page>();
        pageComponent.arguments = arguments;
        pageComponent.Navigation = this;
        pageComponent.Initialize();

        RectTransform panelRectTransform = page.GetComponent<RectTransform>();
        panelRectTransform.anchorMin = Vector2.zero;
        panelRectTransform.anchorMax = Vector2.one;

        panelRectTransform.offsetMin = Vector2.zero;
        panelRectTransform.offsetMax = Vector2.zero;

        if (pageComponent.playAnim)
            pageComponent.GetComponent<Animator>().Play("Show");

        Canvas pageCanvas = page.GetComponent<Canvas>();
        Canvas prevPageCanvas = default;

        if (App.AppModel.NavigationModel.NavigationStack.Count > 0)
        {
            //foreach (GameObject temp in App.AppModel.CanvasModel.Panels)
            //if(temp.GetComponent<PanelViewController>().deactivate)
            //temp.SetActive(false);

            GameObject prevPage = App.AppModel.NavigationModel.NavigationStack.Peek();
            Page prevPageComponent = prevPage.GetComponent<Page>();


            //prevPage.GetComponent<Animator>().SetTrigger("Hide");
            if (prevPageComponent.playAnim)
                prevPage.GetComponent<Animator>().Play("Hide");

            if (prevPageComponent.destroyAtOverlap)
            {
                prevPageComponent.destroyOnHide = true;
                App.AppModel.NavigationModel.NavigationStack.Pop();
                prevPageCanvas = App.AppModel.NavigationModel.NavigationStack.Peek().GetComponent<Canvas>();
            }
            else
            {

                prevPageComponent.destroyOnHide = false;
                prevPageCanvas = prevPage.GetComponent<Canvas>();
            }
        }
        else
            prevPageCanvas = App.AppView.NavigationView.GetComponent<Canvas>();

        pageCanvas.sortingOrder = prevPageCanvas.sortingOrder + 1;
        App.AppModel.NavigationModel.NavigationStack.Push(page);
        App.Notify(EventName.OnPageOpen);

    }
}
