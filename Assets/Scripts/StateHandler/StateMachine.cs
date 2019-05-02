using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Ursprungligen utformad med hjälp av Daniel och senare Max
public class StateMachine
{
    public MonkeyState currentState;
    public MonkeyState newState;
    public Monkey actor;

    public StateMachine(Monkey apa)
    {
        this.actor = apa;
        currentState = null;
    }

    public void ReceiveMessage(MonkeyState.Messages message)
    {
        if (currentState == null)
            return;

        newState = currentState.ReceiveMessage(message);
        CheckStateChange();
    }

    public void Update(float t)
    {
        if (currentState == null)
            return;

        newState = currentState.UpdateState(t);
        CheckStateChange();

    }

    public void CheckStateChange()
    {
        if (newState != null)
        {
            currentState.ExitState();
            currentState = newState;
            currentState.EnterState();
        }
    }

}
