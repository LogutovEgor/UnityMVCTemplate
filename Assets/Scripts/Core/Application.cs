using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using System.Linq;

public class Application : MonoBehaviour
{
    #region Properties

    public ApplicationModel AppModel { get; private set; }
    public ApplicationView AppView { get; private set; }
    public ApplicationController AppController { get; private set; }

    #endregion

    [SerializeField] private List<IInitialNotification> initialEventHandlers = null;
    [SerializeField] private List<INotification> eventHandlers = null;
    [SerializeField] private List<ILateNotification> lateEventHandlers = null;

    [SerializeField] private List<Model> modelsInitializationList;
    [SerializeField] private List<View> viewsInitializationList;
    [SerializeField] private List<Controller> controllersInitializationList;

    private void Start()
    {
        Initialize();
        InitializeElements();
    }
    private void Update()
    {
        InitializeElements();
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
        if (initialEventHandlers != null)
            for (int i = 0; i < initialEventHandlers.Count; i++)
                initialEventHandlers[i].OnInitialNotification(eventName, data);

        if (eventHandlers != null)
            for (int i = 0; i < eventHandlers.Count; i++)
                eventHandlers[i].OnNotification(eventName, data);

        if (lateEventHandlers != null)
            for (int i = 0; i < lateEventHandlers.Count; i++)
                lateEventHandlers[i].OnLateNotification(eventName, data);
    }

    public void AddEventHandler(Controller controller)
    {
        if (controller is IInitialNotification initialHandler)
            if (initialEventHandlers == null)
                initialEventHandlers = new List<IInitialNotification>() { initialHandler };
            else
                initialEventHandlers.Add(initialHandler);
        //
        if (controller is INotification handler)
            if (eventHandlers == null)
                eventHandlers = new List<INotification>() { handler };
            else
                eventHandlers.Add(handler);
        //
        if (controller is ILateNotification lateHadler)
            if (lateEventHandlers == null)
                lateEventHandlers = new List<ILateNotification>() { lateHadler };
            else
                lateEventHandlers.Add(lateHadler);
    }
    public void RemoveEventHandler(Controller controller)
    {
        if (controller is IInitialNotification initialHandler)
            initialEventHandlers.Remove(initialHandler);
        //
        if (controller is INotification handler)
            eventHandlers.Remove(handler);
        //
        if (controller is ILateNotification lateHadler)
            lateEventHandlers.Remove(lateHadler);
    }

    public Object LoadResource(string name)
    {
        return Resources.Load(name);
    }
    private void InitializeElements()
    {
        if (modelsInitializationList.Count > 0)
        {
            for (int i = 0; i < modelsInitializationList.Count; i++)
                modelsInitializationList[i].Initialize();
            modelsInitializationList.Clear();
        }
        if (viewsInitializationList.Count > 0)
        {
            for (int i = 0; i < viewsInitializationList.Count; i++)
                viewsInitializationList[i].Initialize();
            viewsInitializationList.Clear();
        }
        if (controllersInitializationList.Count > 0)
        {
            for (int i = 0; i < controllersInitializationList.Count; i++)
                controllersInitializationList[i].Initialize();
            controllersInitializationList.Clear();
        }
    }

    public void AddModelToinitializationList(Model model)
    {
        if (modelsInitializationList == null)
            modelsInitializationList = new List<Model>() { model };
        else
            modelsInitializationList.Add(model);
    }
    public void AddViewToinitializationList(View view)
    {
        if (viewsInitializationList == null)
            viewsInitializationList = new List<View>() { view };
        else
            viewsInitializationList.Add(view);
    }
    public void AddControllerToinitializationList(Controller controller)
    {
        if (controllersInitializationList == null)
            controllersInitializationList = new List<Controller>() { controller };
        else
            controllersInitializationList.Add(controller);
    }
}
public abstract class Element : MonoBehaviour
{
    private static Application app;
    public static Application App
    {
        get
        {
            if (app == null)
                app = FindObjectOfType<Application>();
            return app;
        }
    }
}
public abstract class Controller : Element
{
    protected bool initialized;
    public virtual void Initialize() { }
    public virtual void DeInitialize() { }

    protected void Awake()
    {
        if (this is IInitialNotification || this is INotification || this is ILateNotification)
            App.AddEventHandler(this);
        if (this is ISelfInitialize)
            App.AddControllerToinitializationList(this);
    }
    protected void OnDestroy()
    {
        if (this is IInitialNotification || this is INotification || this is ILateNotification)
            App.RemoveEventHandler(this);
        DeInitialize();
    }
}
public abstract class View : Element
{
    protected bool initialized;
    public abstract void Initialize();

    protected void Awake()
    {
        if (this is ISelfInitialize)
            App.AddViewToinitializationList(this);
    }
}
public abstract class Model : Element
{
    protected bool initialized;
    public abstract void Initialize();

    protected void Awake()
    {
        if (this is ISelfInitialize)
            App.AddModelToinitializationList(this);
    }
}

public interface INotification
{
    void OnNotification(EventName eventName, params object[] data);
}

public interface IInitialNotification
{
    void OnInitialNotification(EventName eventName, params object[] data);
}

public interface ILateNotification
{
    void OnLateNotification(EventName eventName, params object[] data);
}

public interface ISelfInitialize { }
public interface IResetToDefault
{
    void ResetToDefault();
    System.Object GetDefaultValue();
}