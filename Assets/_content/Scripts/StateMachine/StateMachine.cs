using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using System;

public class StateMachine : Controller
{
    [SerializeField]
    private Controller parentController = default;

    [SerializeField]
    private State currentState = default;

    public override void Initialize(Arguments arguments = null)
    {
        parentController = arguments.Get<Controller>(ArgumentKey.CONTROLLER);
    }

    public void ChangeState(Type newState, Arguments arguments = null)
    {
        if (currentState != null)
        {
            currentState?.ExitState(arguments);
            Destroy(currentState.gameObject);
        }
        //
        currentState = new GameObject($"{newState.Name}", newState).GetComponent<State>();
        currentState.transform.SetParent(ParentController.transform);
        currentState.transform.SetAsFirstSibling();
        if (arguments == null)
            arguments = Arguments.Create();
        currentState.Initialize(arguments.Put(ArgumentKey.STATE_MACHINE, this));
        //
        currentState.EnterState(arguments);
    }

    public void DestroyState()
    {
        if (currentState != null)
        {
            currentState?.ExitState();
            Destroy(currentState.gameObject);
        }
    }
    public Controller ParentController => parentController;
}

//public class StateMachine<T> where T : Controller
//{
//    public T owner;
//    public State<T> CurrentState { get; private set; } = default;

//    public StateMachine(T owner)
//    {
//        this.owner = owner;
//    }
//    public void NotifyCurrentState(EventName eventName, Arguments arguments)
//    {
//        CurrentState.OnNotification(owner, eventName, arguments);
//    }

//    public void ChangeState(State<T> newState, Arguments arguments = null)
//    {
//        CurrentState?.ExitState(owner);
//        CurrentState = newState;
//        CurrentState.EnterState(owner, arguments);
//    }
//    public void Update()
//    {
//        CurrentState?.UpdateState(owner);
//    }
//}
//public abstract class State<T> where T : Controller
//{
//    public abstract void OnNotification(T owner, EventName eventName, Arguments arguments);
//    public abstract void EnterState(T owner, Arguments arguments);
//    public abstract void UpdateState(T owner, Arguments arguments = null);
//    public abstract void ExitState(T owner, Arguments arguments = null);
//}
