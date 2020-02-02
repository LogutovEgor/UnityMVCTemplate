using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public abstract class Page : Controller
{
    public PageName pageName;
    protected INavigation Navigation;

    public GameObject content;


    //public override void Initialize()
    //{
    //}
}

public interface INavigation
{
    void Push(Enums.PageName page);
    void Pop();
    void PopToRoot();
}

