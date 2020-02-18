using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public abstract class Page : Controller
{
    [HideInInspector]
    public NavigationArguments arguments;

    [HideInInspector]
    public PageName pageName;

    [HideInInspector]
    public PageOrientation pageOrientation;

    [HideInInspector]
    public bool destroyOnHide = false;

    protected INavigation Navigation;

    [HideInInspector]
    public Animator animator;

    public bool deactivateOnHide;
    public bool playAnim;

    public override void Initialize()
    {     
        animator = gameObject.GetComponent<Animator>();
    }


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
    void Push(PageName page, NavigationArguments arguments = default);
    //
    void Pop();
    void PopToRoot();
}

public class NavigationArguments
{
    protected Dictionary<string, object> arguments;

    public NavigationArguments()
    {
        arguments = new Dictionary<string, object>();
    }

    public void Put(string key, object value)
    {
        arguments.Add(key, value);
    }

    public object Get(string key)
    {
        if (arguments.ContainsKey(key))
            return arguments[key];
        return null;
    }

    public string GetString(string key)
    {
        return Get(key) as string;
    }

    public int GetInt(string key)
    {
        return (int)Get(key);
    }

    public float GetFloat(string key)
    {
        return (float)Get(key);
    }

    public bool GetBool(string key)
    {
        return (bool)Get(key);
    }
}

