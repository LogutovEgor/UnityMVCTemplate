using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : Controller
{
    public Controller context;
    public State CurrentState { get; private set; } = default;


    public override void Initialize()
    {
        
    }


    public void ChangeState<T>(params object[] data) where T : State
    {
        if (CurrentState != null)
        {
            CurrentState.ExitState();
            Destroy(CurrentState.gameObject);
        }
        GameObject stateGameObject = new GameObject("currentState", typeof(T));
        stateGameObject.transform.SetParent(transform);

        T state = stateGameObject.GetComponent<T>();
        state.StateMachine = this;
        CurrentState = state;
        CurrentState.EnterState(data);
    }
}
public abstract class State : Controller
{
    public StateMachine StateMachine { get; set; }

    public abstract void EnterState(params object[] data);
    public abstract void ExitState(params object[] data);
}