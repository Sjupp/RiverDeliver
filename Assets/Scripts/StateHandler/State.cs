using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public abstract void EnterState(Actor actor);
    public abstract void ExitState(Actor actor);
    public abstract void UpdateState(Actor actor);
}
