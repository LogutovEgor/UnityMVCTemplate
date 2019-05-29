using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class Application : MonoBehaviour
{
    #region Properties

    public ApplicationModel AppModel { get; private set; }
    public ApplicationView AppView { get; private set; }
    public ApplicationController AppController { get; private set; }

    #endregion

    void Awake()
    {
        Initialize();
    }
    private void Initialize()
    {
        AppModel = GetComponentInChildren<ApplicationModel>();
        AppModel.Initialize();
        //
        AppView = GetComponentInChildren<ApplicationView>();
        AppView.Initialize();
        //
        AppController = GetComponentInChildren<ApplicationController>();
        AppController.Initialize();
    }
    public void Notify(EventName eventName, params object[] data)
    {
        Controller[] controllers = FindObjectsOfType<Controller>();

        for (int i = 0; i < controllers.Length; i++)
            controllers[i].OnInitialNotification(eventName, data);

        for (int i = 0; i < controllers.Length; i++)
            controllers[i].OnNotification(eventName, data);

        for (int i = 0; i < controllers.Length; i++)
            controllers[i].OnLateNotification(eventName, data);
    }
    public Object LoadResource(string name)
    {
        return Resources.Load(name);
    }
}
public abstract class Element : MonoBehaviour
{
    private Application app;
    public Application App
    {
        get {
            if (app == null)
                app = FindObjectOfType<Application>();
            return app;
        }
    }
}
public abstract class Controller : Element
{
    public abstract void Initialize();
    public abstract void OnInitialNotification(EventName eventName, params object[] data);
    public abstract void OnNotification(EventName eventName, params object[] data);
    public abstract void OnLateNotification(EventName eventName, params object[] data);
}
public abstract class View : Element
{
    public abstract void Initialize();
}
public abstract class Model : Element
{
    public abstract void Initialize();
}