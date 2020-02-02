using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public abstract class Page : Controller
{
    [HideInInspector]
    public object[] parameters;
    [HideInInspector]
    public PageName pageName;
    [HideInInspector]
    public PageOrientation pageOrientation;
    protected INavigation Navigation;

    public bool deactivateOnHide;
    public bool playAnim;
    [HideInInspector]
    public bool destroyOnHide = false;


    protected virtual void OnShowAnimEnd() { }
    protected virtual void OnHideAnimEnd()
    {
        if (destroyOnHide)
            Destroy(gameObject);
        gameObject.SetActive(!deactivateOnHide);
    }

    //public override void Initialize()
    //{
    //}
}

public interface INavigation
{
    void Push(PageName page, params object[] parameters);
    void Pop();
    void PopToRoot();
}

