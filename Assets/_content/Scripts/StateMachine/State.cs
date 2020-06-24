using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public abstract class State : Controller
{
    private StateMachine stateMachine;

    public override void Initialize(Arguments arguments = null)
    {
        stateMachine = arguments.Get<StateMachine>(ArgumentKey.StateMachine);
        //
    }

    public abstract void EnterState(Arguments arguments = null);
    public abstract void ExitState(Arguments arguments = null);

    protected StateMachine StateMachine => stateMachine;
}
