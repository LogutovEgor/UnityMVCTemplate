using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class Application : MonoBehaviour
{
    public ApplicationModel AppModel { get; private set; }
    public ApplicationView AppView { get; private set; }
    public ApplicationController AppController { get; private set; }

    void Awake()
    {
        Initialize();
    }
    private void Initialize()
    {
        AppModel = GetComponentInChildren<ApplicationModel>();
        AppModel.Initialize();
        AppView = GetComponentInChildren<ApplicationView>();
        AppView.Initialize();
        AppController = GetComponentInChildren<ApplicationController>();
        AppController.Initialize();
    }
    public void Notify(EventName eventName, Object target, params object[] paramsData)
    {
        Controller[] controllers = FindObjectsOfType<Controller>();

        for (int i = 0; i < controllers.Length; i++)
            controllers[i].OnInitialNotification(eventName, target, paramsData);

        for (int i = 0; i < controllers.Length; i++)
            controllers[i].OnNotification(eventName, target, paramsData);

        for (int i = 0; i < controllers.Length; i++)
            controllers[i].OnLateNotification(eventName, target, paramsData);
    }
    public Object LoadResource(string name)
    {
        return Resources.Load(name);
    }
}
public abstract class Element : MonoBehaviour
{
    private Application app;
    public Application App()
    {
        if (app == null)
            app = FindObjectOfType<Application>();
        return app;
    }
}
public abstract class Controller : Element
{
    public abstract void OnInitialNotification(EventName eventName, Object target, params object[] paramsData);
    public abstract void OnNotification(EventName eventName, Object target, params object[] paramsData);
    public abstract void OnLateNotification(EventName eventName, Object target, params object[] paramsData);
    public abstract void Initialize();
}
public abstract class View : Element
{
    public abstract void Initialize();
}
public abstract class Model : Element
{
    public abstract void Initialize();
}