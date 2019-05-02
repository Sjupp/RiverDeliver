using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonkeyState
{
    public enum Messages
    {
        PickUp, PutDown
    }

    public abstract void EnterState();
    public abstract void ExitState();
    public abstract MonkeyState UpdateState(float time);
    public abstract MonkeyState ReceiveMessage(Messages message);
}
