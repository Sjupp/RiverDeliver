using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public State currentState;
    public Actor actor;


    public void ChangeState(State newState)
    {
        if (currentState != null)
            currentState.ExitState(actor);
        currentState = newState;
        currentState.EnterState(actor);
    }

    public void Update()
    {
        if (currentState != null)
            currentState.UpdateState(actor);
    }
}
