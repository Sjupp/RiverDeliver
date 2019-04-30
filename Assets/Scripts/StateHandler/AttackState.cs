using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public override void EnterState(Actor actor)
    {
        Debug.Log("Enter Attack");
    }

    public override void ExitState(Actor actor)
    {
        Debug.Log("Exit Attack");
    }

    public override void UpdateState(Actor actor)
    {
        Debug.Log("Update Attack");
    }
}
