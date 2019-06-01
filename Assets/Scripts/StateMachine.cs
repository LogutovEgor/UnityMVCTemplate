using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class StateMachine<T> where T : Controller
{
    public T owner;
    public State<T> CurrentState { get; private set; } = default;

    public StateMachine(T owner)
    {
        this.owner = owner;
    }
    public void NotifyCurrentState(EventName eventName, params object[] data)
    {
        CurrentState.OnNotification(owner, eventName, data);
    }

    public void ChangeState(State<T> newState, params object[] data)
    {
        if (CurrentState != null)
            CurrentState.ExitState(owner);
        CurrentState = newState;
        CurrentState.EnterState(owner, data);
    }
    public void Update()
    {
        if (CurrentState != null)
            CurrentState.UpdateState(owner);
    }
}
public abstract class State<T> where T : Controller
{
    public abstract void OnNotification(T owner, EventName eventName, params object[] data);
    public abstract void EnterState(T owner, params object[] data);
    public abstract void UpdateState(T owner, params object[] data);
    public abstract void ExitState(T owner, params object[] data);
}
