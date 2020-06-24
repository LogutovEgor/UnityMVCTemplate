using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public abstract class Page : Controller
{
    [HideInInspector]
    public Arguments arguments;

    [HideInInspector]
    public PageName pageName;

    [HideInInspector]
    public PageOrientation pageOrientation;

    [HideInInspector]
    public bool destroyOnHide = false;

    public INavigation Navigation;

    [HideInInspector]
    public Animator animator;

    public bool deactivateOnHide;
    public bool destroyAtOverlap;
    public bool playAnim;

    public override void Initialize(Arguments arguments = default)
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public virtual void OnReturn() { App.Notify(EventName.OnPageOpen); }

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
    void Push(PageName page, Arguments arguments = default);
    //
    void Pop(int count = 1);
}

