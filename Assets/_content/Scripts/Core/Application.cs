using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using System.Linq;
using UnityEngine.UI;


public class Application : MonoBehaviour
{
    #region Properties

    public ApplicationModel AppModel { get; private set; }
    public ApplicationView AppView { get; private set; }
    public ApplicationController AppController { get; private set; }

    #endregion

    public static Application Instance { get; private set; }

    [SerializeField] private List<Controller> initialEventHandlers = null;
    [SerializeField] private List<Controller> eventHandlers = null;
    [SerializeField] private List<Controller> lateEventHandlers = null;

    [SerializeField] private List<Model> modelsInitializationList;
    [SerializeField] private List<View> viewsInitializationList;
    [SerializeField] private List<Controller> controllersInitializationList;

    [SerializeField]
    private Text debugText;
    public void AppendDebugText(string value) => debugText.text += value;

    private void Start()
    {
        Instance = this;
        Initialize();
        InitializeElements();
    }
    private void Update()
    {
        InitializeElements();
    }

    private void Initialize()
    {
        //debugText = GameObject.FindGameObjectWithTag("DebugText").GetComponent<UnityEngine.UI.Text>();
        //try
        //{
        AppModel = GetComponentInChildren<ApplicationModel>();
        AppModel.Initialize();
        InitializeProperties(AppModel);
        //
        AppView = GetComponentInChildren<ApplicationView>();
        AppView.Initialize();
        InitializeProperties(AppView);
        //
        AppController = GetComponentInChildren<ApplicationController>();
        AppController.Initialize();
        InitializeProperties(AppController);
        //}
        // catch(Exception ex)
        // {
        //     debugText.text += ex.ToString();
        // }
        // finally
        // {
        //     debugText.text += "<final>";
        // }

    }

    //Initialize all of child Elements of obj
    public void InitializeProperties(Element obj)
    {
        System.Reflection.PropertyInfo[] propertyInfos = obj
            .GetType()
            .GetProperties()
            .Where(elem => elem.GetCustomAttributes(typeof(NonInitializeAttribute), false).Length == 0 &&

                           (elem.PropertyType.BaseType == typeof(Model) ||
                           elem.PropertyType.BaseType == typeof(View) ||
                           elem.PropertyType.BaseType == typeof(Controller)))
            .ToArray();

        for (int i = 0; i < propertyInfos.Length; i++)
        {
            System.Type type = (propertyInfos[i].PropertyType);
            propertyInfos[i].SetValue(obj, obj.GetComponentInChildren(type));

            Element elem = propertyInfos[i].GetValue(obj) as Element;
            elem.Initialize();
            InitializeProperties(elem);
        }
    }
    public void Notify(EventName eventName, Arguments arguments = null)
    {
        if (initialEventHandlers != null)
            for (int i = 0; i < initialEventHandlers.Count; i++)
                if (initialEventHandlers[i].acceptNotifications)
                    (initialEventHandlers[i] as IInitialNotification).OnInitialNotification(eventName, arguments);

        if (eventHandlers != null)
            for (int i = 0; i < eventHandlers.Count; i++)
                if (eventHandlers[i].acceptNotifications)
                    (eventHandlers[i] as INotification).OnNotification(eventName, arguments);

        if (lateEventHandlers != null)
            for (int i = 0; i < lateEventHandlers.Count; i++)
                if (lateEventHandlers[i].acceptNotifications)
                    (lateEventHandlers[i] as ILateNotification).OnLateNotification(eventName, arguments);
    }

    public void AddEventHandler(Controller controller)
    {
        if (controller is IInitialNotification initialHandler)
            if (initialEventHandlers == null)
                initialEventHandlers = new List<Controller>() { controller };
            else
                initialEventHandlers.Add(controller);
        //
        if (controller is INotification handler)
            if (eventHandlers == null)
                eventHandlers = new List<Controller>() { controller };
            else
                eventHandlers.Add(controller);
        //
        if (controller is ILateNotification lateHadler)
            if (lateEventHandlers == null)
                lateEventHandlers = new List<Controller>() { controller };
            else
                lateEventHandlers.Add(controller);
    }
    public void RemoveEventHandler(Controller controller)
    {
        if (controller is IInitialNotification initialHandler)
            initialEventHandlers.Remove(controller);
        //
        if (controller is INotification handler)
            eventHandlers.Remove(controller);
        //
        if (controller is ILateNotification lateHadler)
            lateEventHandlers.Remove(controller);
    }

    public UnityEngine.Object LoadResource(string name)
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


//initializing
// Initialize
//Serializable
//Initializable
[System.AttributeUsage(System.AttributeTargets.Property | System.AttributeTargets.Field)]
public class InitializeAttribute : System.Attribute
{
}

[System.AttributeUsage(System.AttributeTargets.Property | System.AttributeTargets.Field)]
public class NonInitializeAttribute : System.Attribute
{
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

    protected bool initialized;
    public abstract void Initialize(Arguments arguments = default);
}
public abstract class Controller : Element
{
    //protected bool initialized;
    //public virtual void Initialize() { }
    //public virtual void DeInitialize() { }
    public bool acceptNotifications = true;

    protected void Awake()
    {
        if (this is IInitialNotification || this is INotification || this is ILateNotification)
            App.AddEventHandler(this);
        if (this is ISelfInitialize)
            App.AddControllerToinitializationList(this);
    }
    protected void OnDestroy()
    {
        RemoveEventHandlers();
        //DeInitialize();
    }

    private void RemoveEventHandlers()
    {
        if (this is IInitialNotification || this is INotification || this is ILateNotification)
            App?.RemoveEventHandler(this);
    }


    protected void WaitAndDo(float delay, System.Action action)
    {
        IEnumerator Coroutine()
        {
            yield return new WaitForSeconds(delay);
            action();
        }
        StartCoroutine(Coroutine());
    }

    protected T GetChildrenController<T>() where T : Controller
    {
        T childrenController = gameObject.GetComponentInChildren<T>();
        childrenController?.Initialize();
        return childrenController;
    }

}
public abstract class View : Element
{
    //protected bool initialized;
    //public abstract void Initialize();

    protected void Awake()
    {
        if (this is ISelfInitialize)
            App.AddViewToinitializationList(this);
    }

    protected T GetChildrenView<T>() where T : View
    {
        T childrenView = gameObject.GetComponentInChildren<T>();
        childrenView?.Initialize();
        return childrenView;
    }
}
public abstract class Model : Element
{
    //protected bool initialized;
    //public abstract void Initialize();

    protected void Awake()
    {
        if (this is ISelfInitialize)
            App.AddModelToinitializationList(this);
    }

    protected T GetChildrenModel<T>() where T : Model
    {
        T childrenModel = gameObject.GetComponentInChildren<T>();
        childrenModel?.Initialize();
        return childrenModel;
    }
}

public interface INotification
{
    void OnNotification(EventName eventName, Arguments arguments);
}

public interface IInitialNotification
{
    void OnInitialNotification(EventName eventName, Arguments arguments);
}

public interface ILateNotification
{
    void OnLateNotification(EventName eventName, Arguments arguments);
}

public interface ISelfInitialize { }

public interface IResetToDefault
{
    void ResetToDefault();
}

public interface IToObjectPool : IResetToDefault
{
    void ToObjectPool();
}
