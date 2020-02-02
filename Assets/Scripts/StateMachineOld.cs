using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class StateMachineOld<T> where T : Controller
{
    public T owner;
    public StateOld<T> CurrentState { get; private set; } = default;

    public StateMachineOld(T owner)
    {
        this.owner = owner;
    }
    public void NotifyCurrentState(EventName eventName, params object[] data)
    {
        CurrentState.OnNotification(owner, eventName, data);
    }

    public void ChangeState(StateOld<T> newState, params object[] data)
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
public abstract class StateOld<T> where T : Controller
{
    public abstract void OnNotification(T owner, EventName eventName, params object[] data);
    public abstract void EnterState(T owner, params object[] data);
    public abstract void UpdateState(T owner, params object[] data);
    public abstract void ExitState(T owner, params object[] data);
}
